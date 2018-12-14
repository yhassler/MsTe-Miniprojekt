using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase
    {
        public IList<Kunde> GetAll()
        {
            using (var context = new AutoReservationContext())
            {
                return context.Kunden.ToList();
            }
        }

        public Kunde GetById(int i)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Kunden.Single(a => a.Id == i);
            }
        }

        public void Insert(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(kunde).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, kunde);
                }
            }
        }

        public void Delete(Kunde kunde)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(kunde).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}