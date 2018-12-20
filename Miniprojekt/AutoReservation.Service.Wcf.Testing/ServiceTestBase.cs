using System;
using System.Linq;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
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
            Assert.Throws<FaultException<IllegalIdFault>>(() => Target.GetAutoById(-1));
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException<IllegalIdFault>>(() => Target.GetKundeById(-1));
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.Throws<FaultException<IllegalIdFault>>(() => Target.GetReservationById(-1));
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
            var auto = Target.GetAutoById(1);

            auto.Tagestarif = 60;
            Target.UpdateAuto(auto);
            
            Assert.Equal(60, Target.GetAutoById(1).Tagestarif);
        }

        [Fact]
        public void UpdateKundeTest()
        {
            var kunde = Target.GetKundeById(1);

            kunde.Geburtsdatum = new DateTime(1981, 06, 07);
            Target.UpdateKunde(kunde);

            Assert.Equal(new DateTime(1981, 06, 07), Target.GetKundeById(1).Geburtsdatum);
        }

        [Fact]
        public void UpdateReservationTest()
        {
            var reservation = Target.GetReservationById(4);

            reservation.Bis = new DateTime(2020, 07, 27);
            Target.UpdateReservation(reservation);

            Assert.Equal(new DateTime(2020, 07, 27), Target.GetReservationById(4).Bis);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            var auto1 = Target.GetAutoById(1);
            var auto2 = Target.GetAutoById(1);

            auto1.Tagestarif = 60;
            auto2.Tagestarif = 60;
            Target.UpdateAuto(auto1);

            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateAuto(auto2));
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            var kunde1 = Target.GetKundeById(1);
            var kunde2 = Target.GetKundeById(1);

            kunde1.Geburtsdatum = new DateTime(1981, 06, 07);
            kunde2.Geburtsdatum = new DateTime(1981, 06, 07);
            Target.UpdateKunde(kunde1);

            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateKunde(kunde2));
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            var reservation1 = Target.GetReservationById(4);
            var reservation2 = Target.GetReservationById(4);

            reservation1.Bis = new DateTime(2020, 07, 27);
            reservation2.Bis = new DateTime(2020, 07, 27);
            Target.UpdateReservation(reservation1);

            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateReservation(reservation2));
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            var reservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2099, 09, 01),
                Bis = new DateTime(2099, 09, 01)
            };

            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => Target.InsertReservation(reservation));
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            var reservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(1999, 09, 01),
                Bis = new DateTime(2099, 09, 01)
            };

            Assert.Throws<FaultException<AutoUnavailableFault>>(() => Target.InsertReservation(reservation));
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            var reservation = Target.GetReservationById(4);

            reservation.Von = new DateTime(2020, 07, 27);
            reservation.Bis = new DateTime(2020, 07, 27);

            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => Target.UpdateReservation(reservation));
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            var reservation = Target.GetReservationById(4);

            reservation.Von = new DateTime(1920, 07, 27);
            reservation.Bis = new DateTime(2120, 07, 26);

            Assert.Throws<FaultException<AutoUnavailableFault>>(() => Target.UpdateReservation(reservation));
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            var reservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(2095, 09, 01),
                Bis = new DateTime(2095, 09, 02)
            };
            
            Assert.True(Target.IsAutoAvailable(reservation));
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            var reservation = new ReservationDto
            {
                Auto = Target.GetAutoById(1),
                Kunde = Target.GetKundeById(1),
                Von = new DateTime(1995, 09, 01),
                Bis = new DateTime(2095, 08, 31)
            };

            Assert.False(Target.IsAutoAvailable(reservation));
        }

        #endregion
    }
}
