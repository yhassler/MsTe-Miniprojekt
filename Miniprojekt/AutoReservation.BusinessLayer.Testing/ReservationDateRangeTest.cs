using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        [Fact]
        public void OneDayTest()
        {
            ReservationManager.CheckDateRange(new DateTime(2000, 01, 01), new DateTime(2000, 01, 02));
        }

        [Fact]
        public void OneYearTest()
        {
            ReservationManager.CheckDateRange(new DateTime(2000, 01, 01), new DateTime(2000, 12, 31));
        }

        [Fact]
        public void EqualDateTimeTest()
        {
            Assert.Throws<InvalidDateRangeException>(() =>
                ReservationManager.CheckDateRange(new DateTime(2000, 01, 01), new DateTime(2000, 01, 01)));
        }

        [Fact]
        public void FewHoursTest()
        {
            Assert.Throws<InvalidDateRangeException>(() =>
                ReservationManager.CheckDateRange(new DateTime(2000, 01, 01, 00, 00, 00), new DateTime(2000, 01, 01, 23, 59, 59)));
        }

        [Fact]
        public void NegativeDateRangeTest()
        {
            Assert.Throws<InvalidDateRangeException>(() =>
                ReservationManager.CheckDateRange(new DateTime(2000, 02, 01), new DateTime(2000, 01, 01)));
        }
    }
}
