using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf.Testing
{
    public class ServiceTestLocal 
        : ServiceTestBase
    {
        private IAutoReservationService target;
        protected override IAutoReservationService Target => target ?? (target = new AutoReservationService());
    }
}