using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Kunde
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Vorname { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nachname { get; set; }

        [Required]
        public DateTime Geburtsdatum { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [InverseProperty(nameof(Reservation.Kunde))]
        public ICollection<Reservation> Reservationen { get; set; }
    }
}
