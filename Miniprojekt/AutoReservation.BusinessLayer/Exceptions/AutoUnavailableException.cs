using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class AutoUnavailableException 
        : Exception
    {
        public AutoUnavailableException(string message) : base(message) { }
        public AutoUnavailableException(string message, int autoId, DateTime from, DateTime to) : base(message)
        {
            AutoId = autoId;
            From = from;
            To = to;
        }

        public int AutoId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}