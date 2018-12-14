using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private AutoManager AutoManager { get; set; }
        private KundeManager KundeManager { get; set; }
        private ReservationManager ReservationManager { get; set; }

        public AutoReservationService()
        {
            AutoManager = new AutoManager();
            KundeManager = new KundeManager();
            ReservationManager = new ReservationManager();
        }

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        #region Auto

        public AutoDto GetAutoById(int id)
        {
            return AutoManager.GetById(id).ConvertToDto();
        }

        public IList<AutoDto> GetAutos()
        {
            return AutoManager.GetAll().ConvertToDtos();
        }

        public void InsertAuto(AutoDto auto)
        {
            AutoManager.Insert(auto.ConvertToEntity());
        }

        public void UpdateAuto(AutoDto auto)
        {
            AutoManager.Update(auto.ConvertToEntity());
        }

        public void DeleteAuto(AutoDto auto)
        {
            AutoManager.Delete(auto.ConvertToEntity());
        }

        #endregion

        #region Kunde

        public KundeDto GetKundeById(int id)
        {
            return KundeManager.GetById(id).ConvertToDto();
        }

        public IList<KundeDto> GetKunden()
        {
            return KundeManager.GetAll().ConvertToDtos();
        }

        public void InsertKunde(KundeDto kunde)
        {
            KundeManager.Insert(kunde.ConvertToEntity());
        }

        public void UpdateKunde(KundeDto kunde)
        {
            KundeManager.Update(kunde.ConvertToEntity());
        }

        public void DeleteKunde(KundeDto kunde)
        {
            KundeManager.Delete(kunde.ConvertToEntity());
        }

        #endregion

        #region Reservation

        public ReservationDto GetReservationById(int id)
        {
            return ReservationManager.GetById(id).ConvertToDto();
        }

        public IList<ReservationDto> GetReservationen()
        {
            return ReservationManager.GetAll().ConvertToDtos();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            ReservationManager.Insert(reservation.ConvertToEntity());
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            ReservationManager.Update(reservation.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            ReservationManager.Delete(reservation.ConvertToEntity());
        }

        #endregion

        #region Checks

        public bool IsAutoAvailable(ReservationDto reservation)
        {
            return ReservationManager.IsAutoAvailable(reservation.ConvertToEntity());
        }

        #endregion

    }
}