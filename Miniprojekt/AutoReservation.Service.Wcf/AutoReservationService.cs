using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using AutoReservation.BusinessLayer;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
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
            WriteActualMethod();
            return AutoManager.GetById(id).ConvertToDto();
        }

        public IList<AutoDto> GetAutos()
        {
            WriteActualMethod();
            return AutoManager.GetAll().ConvertToDtos();
        }

        public void InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            AutoManager.Insert(auto.ConvertToEntity());
        }

        public void UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            { AutoManager.Update(auto.ConvertToEntity()); }
            catch (OptimisticConcurrencyException<AutoDto>)
            {
                OptimisticConcurrencyFault of = new OptimisticConcurrencyFault
                {
                    Issue = "Datenintegritätsfehler",
                    Details = "Das Objekt wird bereits von einer anderen Person bearbeitet."
                };
                throw new FaultException<OptimisticConcurrencyFault>(of);
            }
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            AutoManager.Delete(auto.ConvertToEntity());
        }

        #endregion

        #region Kunde

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            return KundeManager.GetById(id).ConvertToDto();
        }

        public IList<KundeDto> GetKunden()
        {
            WriteActualMethod();
            return KundeManager.GetAll().ConvertToDtos();
        }

        public void InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            KundeManager.Insert(kunde.ConvertToEntity());
        }

        public void UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try { KundeManager.Update(kunde.ConvertToEntity()); }
            catch (OptimisticConcurrencyException<KundeDto>)
            {
                OptimisticConcurrencyFault of = new OptimisticConcurrencyFault
                {
                    Issue = "Datenintegritätsfehler",
                    Details = "Das Objekt wird bereits von einer anderen Person bearbeitet."
                };
                throw new FaultException<OptimisticConcurrencyFault>(of);
            }
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            KundeManager.Delete(kunde.ConvertToEntity());
        }

        #endregion

        #region Reservation

        public ReservationDto GetReservationById(int id)
        {
            WriteActualMethod();
            return ReservationManager.GetById(id).ConvertToDto();
        }

        public IList<ReservationDto> GetReservationen()
        {
            WriteActualMethod();
            return ReservationManager.GetAll().ConvertToDtos();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try { ReservationManager.Insert(reservation.ConvertToEntity()); }
            catch (InvalidDateRangeException)
            {
                InvalidDateRangeFault idf = new InvalidDateRangeFault
                {
                    Issue = "Ungültiges Datum",
                    Details = "Das angegebene Datum ist ungültig."
                };
                throw new FaultException<InvalidDateRangeFault>(idf);
            }
            catch (AutoUnavailableException)
            {
                AutoUnavailableFault auf = new AutoUnavailableFault
                {
                    Issue = "Auto nicht verfügbar",
                    Details = "Das ausgewählte Auto is zur gewünschten Zeit nicht verfügbar."
                };
                throw new FaultException<AutoUnavailableFault>(auf);
            }
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try { ReservationManager.Update(reservation.ConvertToEntity()); }
            catch (OptimisticConcurrencyException<AutoDto>)
            {
                OptimisticConcurrencyFault of = new OptimisticConcurrencyFault
                {
                    Issue = "Datenintegritätsfehler",
                    Details = "Das Objekt wird bereits von einer anderen Person bearbeitet."
                };
                throw new FaultException<OptimisticConcurrencyFault>(of);
            }
            catch (InvalidDateRangeException)
            {
                InvalidDateRangeFault idf = new InvalidDateRangeFault
                {
                    Issue = "Ungültiges Datum",
                    Details = "Das angegebene Datum ist ungültig."
                };
                throw new FaultException<InvalidDateRangeFault>(idf);
            }
            catch (AutoUnavailableException)
            {
                AutoUnavailableFault auf = new AutoUnavailableFault
                {
                    Issue = "Auto nicht verfügbar",
                    Details = "Das ausgewählte Auto is zur gewünschten Zeit nicht verfügbar."
                };
                throw new FaultException<AutoUnavailableFault>(auf);
            }
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            ReservationManager.Delete(reservation.ConvertToEntity());
        }

        #endregion

        #region Checks

        public bool IsAutoAvailable(ReservationDto reservation)
        {
            WriteActualMethod();
            return ReservationManager.IsAutoAvailable(reservation.ConvertToEntity());
        }

        #endregion

    }
}