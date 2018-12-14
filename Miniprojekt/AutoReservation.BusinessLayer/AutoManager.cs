using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager
        : ManagerBase
    {
        public IList<Auto> GetAll()
        {
            using (var context = new AutoReservationContext())
            {
                return context.Autos.ToList();
            }
        }

        public Auto GetById(int i)
        {
            using (var context = new AutoReservationContext())
            {
                return context.Autos.Single(a => a.Id == i);
            }
        }

        public void Insert(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(auto).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateOptimisticConcurrencyException(context, auto);
                }
            }
        }

        public void Delete(Auto auto)
        {
            using (var context = new AutoReservationContext())
            {
                context.Entry(auto).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}