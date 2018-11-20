using System;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase
    {
        protected abstract IAutoReservationService Target { get; }

        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundenTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetReservationenTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void DeleteKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void DeleteReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion
    }
}
