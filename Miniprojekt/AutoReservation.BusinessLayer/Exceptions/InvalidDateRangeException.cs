using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class InvalidDateRangeException 
        : Exception
    {
        public InvalidDateRangeException(string message) : base(message) { }
        public InvalidDateRangeException(string message, DateTime from, DateTime to) : base(message)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}