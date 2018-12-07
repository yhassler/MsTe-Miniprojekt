using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class KundeDto
    {
        public int Id { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public DateTime Geburtsdatum { get; set; }

        public byte[] RowVersion { get; set; }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {RowVersion}";
    }
}
