using System;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());

        [Fact]
        public void UpdateKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }
    }
}
