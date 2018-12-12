using System;
using System.Collections.Generic;
using System.Linq;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase
    {
        #region Reservation

        public IList<Reservation> GetAll()
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen.ToList();
            }
        }

        public Reservation GetById(int i)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Reservationen.Single(r => r.ReservationsNr == i);
            }
        }

        public void Insert(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                CheckDateRange(reservation.Von, reservation.Bis);
                CheckAvailability(context, reservation.Auto, reservation.Von, reservation.Bis);

                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    CheckDateRange(reservation.Von, reservation.Bis);
                    CheckAvailability(context, reservation.Auto, reservation.Von, reservation.Bis);

                    context.Entry(reservation).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }

        public void Delete(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        #endregion

        #region Checks

        public static void CheckDateRange(DateTime from, DateTime to)
        {
            if (to.Subtract(from).TotalHours < 24)
            {
                throw new InvalidDateRangeException("A reservation must take at least 24 hours", from, to);
            }
        }

        public static void CheckAvailability(AutoReservationContext context, Auto auto, DateTime from, DateTime to)
        {
            if (!IsAutoAvailable(context, auto, from, to))
            {
                var msg = $"The car of the make \"${auto.Marke}\" is not available for the selected timespan";
                throw new AutoUnavailableException(msg, auto.Id, from, to);
            }
        }

        public static bool IsAutoAvailable(Auto auto, DateTime from, DateTime to)
        {
            using (var context = new AutoReservationContext())
            {
                return IsAutoAvailable(context, auto, from, to);
            }
        }

        private static bool IsAutoAvailable(AutoReservationContext context, Auto auto, DateTime from, DateTime to)
        {
            return !context.Reservationen
                .Any(r => r.Auto == auto && r.Von < to && from < r.Bis);
        }

        #endregion
    }
}