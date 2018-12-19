using System;
using System.Runtime.Serialization;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Vorname { get; set; }

        [DataMember]
        public string Nachname { get; set; }

        [DataMember]
        public DateTime Geburtsdatum { get; set; }

        [DataMember]
        public byte[] RowVersion { get; set; }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}";
    }
}
