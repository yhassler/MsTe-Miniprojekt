using System.Linq;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoTests
        : TestBase
    {
        private AutoManager _target;
        private AutoManager Target => _target ?? (_target = new AutoManager());

        [Fact]
        public void GetAutoTest()
        {
            var auto = Target.GetById(1);
            Assert.Equal(50, auto.Tagestarif);
        }

        [Fact]
        public void UpdateAutoTest()
        {
            var auto = Target.GetById(1);

            auto.Tagestarif = 25;
            Target.Update(auto);

            var updatedAuto = Target.GetById(1);
            Assert.Equal(25, updatedAuto.Tagestarif);
        }

        [Fact]
        public void InsertAutoTest()
        {
            var auto = new StandardAuto { Marke = "Fiat 500 Limited", Tagestarif = 999 };

            Target.Insert(auto);

            var inserted = Target.GetAll().First(a => a.Marke == "Fiat 500 Limited");
            Assert.True(4 < inserted.Id);
            Assert.Equal(999, inserted.Tagestarif);
        }

        [Fact]
        public void DeleteAutoTest()
        {
            var auto = Target.GetById(4);

            Target.Delete(auto);

            Assert.Equal(0, Target.GetAll().Count(a => a.Id == 4));
        }
    }
}
