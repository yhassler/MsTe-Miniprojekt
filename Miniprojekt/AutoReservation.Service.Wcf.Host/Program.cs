using System;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AutoReservationService starting.");
            
            // Instantiate new ServiceHost 
            ServiceHost host = new ServiceHost(typeof(AutoReservationService));

            // Open ServiceHost
            host.Open();

            Console.WriteLine("AutoReservationService started.");
            Console.WriteLine();
            Console.WriteLine("Press Return to stop the Service.");

            Console.ReadLine();

            // Stop ServiceHost
            if (host.State != CommunicationState.Closed)
            {
                host.Close();
            }
        }
    }
}
