using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        IList<AutoDto> GetAutos();

        AutoDto GetAutoById(int id);

        void InsertAuto(AutoDto auto);

        void UpdateAuto(AutoDto auto);

        void DeleteAuto(AutoDto auto);

        IList<KundeDto> GetKunden();

        KundeDto GetKundeById(int id);

        void InsertKunde(KundeDto kunde);

        void UpdateKunde(KundeDto kunde);

        void DeleteKunde(KundeDto kunde);

        IList<ReservationDto> GetReservationen();

        ReservationDto GetReservationById(int id);

        void InsertReservation(ReservationDto reservation);

        void UpdateReservation(ReservationDto reservation);

        void DeleteReservation(ReservationDto reservation);

        /// <summary>
        /// Retrieves if a car is available at a later point in time.
        /// </summary>
        /// <param name="reservation">The reservation containing the car and timespan to check.</param>
        /// <returns>True, if the reservation doesn't lead to a time collision.</returns>
        bool IsAutoAvailable(ReservationDto reservation);
    }
}
