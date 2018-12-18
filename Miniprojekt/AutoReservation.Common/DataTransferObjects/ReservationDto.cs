using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto
    {
        [DataMember]
        public int ReservationsNr { get; set; }

        [DataMember]
        public AutoDto Auto { get; set; }

        [DataMember]
        public KundeDto Kunde { get; set; }

        [DataMember]
        public DateTime Von { get; set; }

        [DataMember]
        public DateTime Bis { get; set; }

        [DataMember]
        public byte[] RowVersion { get; set; }

        public override string ToString()
            => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";
    }
}
