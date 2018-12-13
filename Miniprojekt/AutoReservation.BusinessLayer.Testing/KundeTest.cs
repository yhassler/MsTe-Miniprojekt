using System;
using System.Linq;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeTest
        : TestBase
    {
        private KundeManager _target;
        private KundeManager Target => _target ?? (_target = new KundeManager());

        [Fact]
        public void GetKundeTest()
        {
            var kunde = Target.GetById(1);
            Assert.Equal("Anna", kunde.Vorname);
        }

        [Fact]
        public void UpdateKundeTest()
        {
            var kunde = Target.GetById(1);

            kunde.Geburtsdatum = new DateTime(1999, 11, 11);
            Target.Update(kunde);

            var updatedKunde = Target.GetById(1);
            Assert.Equal(new DateTime(1999, 11, 11), updatedKunde.Geburtsdatum);
        }

        [Fact]
        public void InsertKundeTest()
        {
            var kunde = new Kunde { Nachname = "Schub", Vorname = "Karen", Geburtsdatum = new DateTime(1985, 01, 31) };

            Target.Insert(kunde);

            var inserted = Target.GetAll().First(a => a.Vorname == "Karen");
            Assert.True(4 < inserted.Id);
            Assert.Equal(new DateTime(1985, 01, 31), inserted.Geburtsdatum);
        }

        [Fact]
        public void DeleteKundeTest()
        {
            var kunde = Target.GetById(3);

            Target.Delete(kunde);

            Assert.Equal(0, Target.GetAll().Count(a => a.Id == 3));
        }
    }
}
