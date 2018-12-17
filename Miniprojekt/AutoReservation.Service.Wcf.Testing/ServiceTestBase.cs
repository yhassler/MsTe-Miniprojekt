using System;
using System.Linq;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
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
            var autos = Target.GetAutos();

            Assert.NotEmpty(autos);
            Assert.Contains(autos, a => a.Marke == "Audi S6");
        }

        [Fact]
        public void GetKundenTest()
        {
            var kunden = Target.GetKunden();

            Assert.NotEmpty(kunden);
            Assert.Contains(kunden, k => k.Nachname == "Beil");
        }

        [Fact]
        public void GetReservationenTest()
        {
            var reservationen = Target.GetReservationen();

            Assert.NotEmpty(reservationen);
            Assert.Contains(reservationen, r => r.Von == new DateTime(2020, 01, 10));
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            var auto = Target.GetAutoById(1);

            Assert.Equal(1, auto.Id);
            Assert.Equal("Fiat Punto", auto.Marke);
            Assert.Equal(50, auto.Tagestarif);
            Assert.Equal(AutoKlasse.Standard, auto.AutoKlasse);
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            var kunde = Target.GetKundeById(1);

            Assert.Equal(1, kunde.Id);
            Assert.Equal("Anna", kunde.Vorname);
            Assert.Equal("Nass", kunde.Nachname);
            Assert.Equal(new DateTime(1981, 05, 05), kunde.Geburtsdatum);
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            var reservation = Target.GetReservationById(1);

            Assert.Equal(1, reservation.ReservationsNr);
            Assert.Equal(new DateTime(2020, 01, 10), reservation.Von);
            Assert.Equal(new DateTime(2020, 01, 20), reservation.Bis);
            Assert.Equal("Fiat Punto", reservation.Auto.Marke);
            Assert.Equal("Nass", reservation.Kunde.Nachname);
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException>(() => Target.GetAutoById(-1));
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException>(() => Target.GetKundeById(-1));
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.Throws<FaultException>(() => Target.GetReservationById(-1));
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            var auto = new AutoDto
            {
                AutoKlasse = AutoKlasse.Luxusklasse,
                Basistarif = 3000000,
                Marke = "Tesla Model S",
                Tagestarif = 1
            };

            Target.InsertAuto(auto);

            Assert.Contains(Target.GetAutos(), a => a.Marke == "Tesla Model S");
        }

        [Fact]
        public void InsertKundeTest()
        {
            var kunde = new KundeDto
            {
                Vorname = "Peter",
                Nachname = "File",
                Geburtsdatum = new DateTime(1971, 02, 23)
            };

            Target.InsertKunde(kunde);

            Assert.Contains(Target.GetKunden(), k => k.Vorname == "Peter");
        }

        [Fact]
        public void InsertReservationTest()
        {
            var reservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2099, 09, 01),
                Bis = new DateTime(2099, 09, 03)
            };

            Target.InsertReservation(reservation);

            Assert.Contains(Target.GetReservationen(), r => r.Von == new DateTime(2099, 09, 01));
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            var auto = new AutoDto
            {
                AutoKlasse = AutoKlasse.Luxusklasse,
                Basistarif = 1000,
                Marke = "Audi A8",
                Tagestarif = 20
            };
            Target.InsertAuto(auto);

            var autoDto = Target.GetAutos().First(a => a.Marke == "Audi A8");
            Target.DeleteAuto(autoDto);

            Assert.DoesNotContain(Target.GetAutos(), a => a.Marke == "Audi A8");
        }

        [Fact]
        public void DeleteKundeTest()
        {
            var kunde = new KundeDto
            {
                Vorname = "Sum Ting",
                Nachname = "Wong",
                Geburtsdatum = new DateTime(1986, 05, 28)
            };
            Target.InsertKunde(kunde);

            var kundeDto = Target.GetKunden().First(k => k.Nachname == "Wong");
            Target.DeleteKunde(kundeDto);

            Assert.DoesNotContain(Target.GetKunden(), k => k.Nachname == "Wong");
        }

        [Fact]
        public void DeleteReservationTest()
        {
            var reservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2098, 12, 26),
                Bis = new DateTime(2098, 12, 31)
            };
            Target.InsertReservation(reservation);

            var reservationDto = Target.GetReservationen().First(r => r.Von == new DateTime(2098, 12, 26));
            Target.DeleteReservation(reservationDto);

            Assert.DoesNotContain(Target.GetReservationen(), r => r.Von == new DateTime(2098, 12, 26));
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
