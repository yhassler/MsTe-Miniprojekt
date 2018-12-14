using System;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest
        : TestBase
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        #region Reservation available

        public ReservationAvailabilityTest()
        {
            // Prepare reservation
            Reservation reservation = Target.GetById(1);
            reservation.Von = DateTime.Today;
            reservation.Bis = DateTime.Today.AddDays(7);
            Target.Update(reservation);
        }

        [Fact]
        public void SimpleAvailableReservationTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = new DateTime(1990, 07, 01),
                Bis = new DateTime(1990, 07, 14)
            };

            Assert.True(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void SimpleAvailableReservationWithoutExceptionTest()
        {
            using (var context = new AutoReservationContext())
            {
                var reservation = new Reservation
                {
                    ReservationsNr = -1,
                    AutoId = 1,
                    Von = new DateTime(1990, 07, 01),
                    Bis = new DateTime(1990, 07, 14)
                };

                ReservationManager.CheckAvailability(context, reservation);
            }
        }

        [Fact]
        public void AvailableBeforeOtherTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today.AddDays(-1),
                Bis = DateTime.Today
            };

            Assert.True(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void AvailableAfterOtherTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today.AddDays(7),
                Bis = DateTime.Today.AddDays(8)
            };

            Assert.True(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void AvailableEvenIfSameReservationTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = 1,
                AutoId = 1,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(14)
            };

            Assert.True(ReservationManager.IsAutoAvailable(reservation));
        }

        #endregion

        #region Reservation not available

        [Fact]
        public void OverlapAtTheStartTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today.AddDays(-1),
                Bis = DateTime.Today.AddDays(1)
            };

            Assert.False(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void OverlapAtTheEndTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today.AddDays(6),
                Bis = DateTime.Today.AddDays(8)
            };

            Assert.False(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void SameStartDateTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today,
                Bis = DateTime.Today.AddDays(2)
            };

            Assert.False(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void SameEndDateTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today.AddDays(5),
                Bis = DateTime.Today.AddDays(7)
            };

            Assert.False(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void OverlapInTheMiddleTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today.AddDays(3),
                Bis = DateTime.Today.AddDays(4)
            };

            Assert.False(ReservationManager.IsAutoAvailable(reservation));
        }

        [Fact]
        public void LongOverlapTest()
        {
            var reservation = new Reservation
            {
                ReservationsNr = -1,
                AutoId = 1,
                Von = DateTime.Today.AddDays(-14),
                Bis = DateTime.Today.AddDays(14)
            };

            Assert.False(ReservationManager.IsAutoAvailable(reservation));
        }

        #endregion
    }
}
