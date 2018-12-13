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
                CheckAvailability(context, reservation);

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
                    CheckAvailability(context, reservation);

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

        public static void CheckAvailability(AutoReservationContext context, Reservation reservation)
        {
            if (!IsAutoAvailable(context, reservation))
            {
                var msg = $"The car with id {reservation.AutoId} is not available for the selected timespan";
                throw new AutoUnavailableException(msg, reservation.AutoId, reservation.Von, reservation.Bis);
            }
        }

        public static bool IsAutoAvailable(Reservation reservation)
        {
            using (var context = new AutoReservationContext())
            {
                return IsAutoAvailable(context, reservation);
            }
        }

        private static bool IsAutoAvailable(AutoReservationContext context, Reservation reservation)
        {
            var resNr = reservation.ReservationsNr;
            var autoId = reservation.AutoId;
            var from = reservation.Von;
            var to = reservation.Bis;

            return !context.Reservationen
                .Any(r => r.ReservationsNr != resNr && r.AutoId == autoId && r.Von < to && from < r.Bis);
        }

        #endregion
    }
}