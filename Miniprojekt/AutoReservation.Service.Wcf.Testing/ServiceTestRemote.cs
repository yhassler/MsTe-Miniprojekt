using System;
using System.ServiceModel;
using AutoReservation.Common.Interfaces;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public class ServiceTestRemote
        : ServiceTestBase
        , IClassFixture<ServiceTestRemoteFixture>
    {
        private readonly ServiceTestRemoteFixture serviceTestRemoteFixture;

        public ServiceTestRemote(ServiceTestRemoteFixture serviceTestRemoteFixture)
        {
            this.serviceTestRemoteFixture = serviceTestRemoteFixture;
        }

        private IAutoReservationService target;
        protected override IAutoReservationService Target
        {
            get
            {
                if (target == null)
                {
                    ChannelFactory<IAutoReservationService> channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
                    target = channelFactory.CreateChannel();
                }
                return target;
            }
        }
    }

    public class ServiceTestRemoteFixture
        : IDisposable
    {
        public ServiceTestRemoteFixture()
        {
            ServiceHost = new ServiceHost(typeof(AutoReservationService));
            ServiceHost.Open();
        }

        public void Dispose()
        {
            if (ServiceHost.State != CommunicationState.Closed)
            {
                ServiceHost.Close();
            }
        }

        public ServiceHost ServiceHost { get; }
    }
}