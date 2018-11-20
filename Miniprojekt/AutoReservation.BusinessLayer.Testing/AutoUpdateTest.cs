using System;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void UpdateAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }
    }
}
