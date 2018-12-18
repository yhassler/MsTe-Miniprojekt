using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {

        #region Auto

        [OperationContract]
        IList<AutoDto> GetAutos();

        [OperationContract]
        AutoDto GetAutoById(int id);

        [OperationContract]
        void InsertAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void UpdateAuto(AutoDto auto);

        [OperationContract]
        void DeleteAuto(AutoDto auto);

        #endregion

        #region Kunde

        [OperationContract]
        IList<KundeDto> GetKunden();

        [OperationContract]
        KundeDto GetKundeById(int id);

        [OperationContract]
        void InsertKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void UpdateKunde(KundeDto kunde);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);

        #endregion

        #region Reservation

        [OperationContract]
        IList<ReservationDto> GetReservationen();

        [OperationContract]
        ReservationDto GetReservationById(int id);

        [OperationContract]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(AutoUnavailableFault))]
        void InsertReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(AutoUnavailableFault))]
        void UpdateReservation(ReservationDto reservation);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);

        #endregion

        #region Checks

        /// <summary>
        /// Retrieves if a car is available at a later point in time.
        /// </summary>
        /// <param name="reservation">The reservation containing the car and timespan to check.</param>
        /// <returns>True, if the reservation doesn't lead to a time collision.</returns>

        [OperationContract]
        bool IsAutoAvailable(ReservationDto reservation);

        #endregion
    }
}
