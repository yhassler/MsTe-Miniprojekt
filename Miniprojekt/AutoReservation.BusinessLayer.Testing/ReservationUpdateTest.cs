using System;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void UpdateReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }
    }
}
