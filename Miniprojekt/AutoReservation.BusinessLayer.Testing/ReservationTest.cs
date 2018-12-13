using System;
using System.Linq;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationTest
        : TestBase
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [Fact]
        public void GetReservationTest()
        {
            var reservation = Target.GetById(1);
            Assert.Equal(1, reservation.AutoId);
        }

        [Fact]
        public void UpdateReservationTest()
        {
            var reservation = Target.GetById(4);

            reservation.Bis = new DateTime(2020, 06, 20);
            Target.Update(reservation);

            var updatedReservation = Target.GetById(4);
            Assert.Equal(new DateTime(2020, 06, 20), updatedReservation.Bis);
        }

        [Fact]
        public void InsertReservationTest()
        {
            var reservation = new Reservation
            { AutoId = 1, KundeId = 1, Von = new DateTime(2021, 01, 01), Bis = new DateTime(2021, 01, 31) };

            Target.Insert(reservation);

            var inserted = Target.GetAll().First(a => a.Von == new DateTime(2021, 01, 01));
            Assert.True(4 < inserted.ReservationsNr);
            Assert.Equal(1, inserted.AutoId);
        }

        [Fact]
        public void DeleteReservationTest()
        {
            var reservation = Target.GetById(3);

            Target.Delete(reservation);

            Assert.Equal(0, Target.GetAll().Count(a => a.ReservationsNr == 3));
        }
    }
}
