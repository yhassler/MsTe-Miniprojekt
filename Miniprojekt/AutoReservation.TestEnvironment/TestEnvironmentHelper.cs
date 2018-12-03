using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutoReservation.TestEnvironment
{
    public static class TestEnvironmentHelper
    {
        private const string InitializationError = "Error while re-initializing database entries.";

        public static void InitializeTestData(this AutoReservationContext context)
        {
            string luxusklasseAutoTableName = context.GetTableName<LuxusklasseAuto>();
            string mittelklasseAutoTableName = context.GetTableName<MittelklasseAuto>();
            string standardAutoTableName = context.GetTableName<StandardAuto>();
            string autoTableName = context.GetTableName<Auto>();
            string kundeTableName = context.GetTableName<Kunde>();
            string reservationTableName = context.GetTableName<Reservation>();

            try
            {
                // Delete all records from tables
                //      > Cleanup for specific subtypes necessary when not using table per hierarchy (TPH)
                //        since entities will be stored in different tables.
                context.DeleteAllRecords(reservationTableName);
                if (luxusklasseAutoTableName != autoTableName)
                {
                    context.DeleteAllRecords(luxusklasseAutoTableName);
                }
                if (mittelklasseAutoTableName != autoTableName)
                {
                    context.DeleteAllRecords(mittelklasseAutoTableName);
                }
                if (standardAutoTableName != autoTableName)
                {
                    context.DeleteAllRecords(standardAutoTableName);
                }
                context.DeleteAllRecords(autoTableName);
                context.DeleteAllRecords(kundeTableName);

                SeedAuto(context, luxusklasseAutoTableName, mittelklasseAutoTableName, standardAutoTableName, autoTableName);
                SeedKunde(context, kundeTableName);
                SeedReservation(context, reservationTableName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(InitializationError, ex);
            }
        }

        private static void SeedAuto(
            AutoReservationContext context,
            string luxusklasseAutoTableName,
            string mittelklasseAutoTableName,
            string standardAutoTableName,
            string autoTableName)
        {
            try
            {
                // Temporarily allow insertion of identity columns (Id)
                context.SetAutoIncrementOnTable(luxusklasseAutoTableName, true);
                context.SetAutoIncrementOnTable(mittelklasseAutoTableName, true);
                context.SetAutoIncrementOnTable(standardAutoTableName, true);
                context.SetAutoIncrementOnTable(autoTableName, true);

                // Insert test data
                context.Autos.AddRange(Autos);
                context.SaveChanges();
            }

            finally
            {
                // Disable insertion of identity columns (Id)
                context.SetAutoIncrementOnTable(luxusklasseAutoTableName, false);
                context.SetAutoIncrementOnTable(mittelklasseAutoTableName, false);
                context.SetAutoIncrementOnTable(standardAutoTableName, false);
                context.SetAutoIncrementOnTable(autoTableName, false);
            }
        }

        private static void SeedKunde(
            AutoReservationContext context,
            string kundeTableName)
        {
            try
            {
                // Reset the identity seed (Id's will start again from 1)
                context.ResetEntitySeed(kundeTableName);

                // Temporarily allow insertion of identity columns (Id)
                context.SetAutoIncrementOnTable(kundeTableName, true);

                // Insert test data
                context.Kunden.AddRange(Kunden);
                context.SaveChanges();
            }
            finally
            {
                // Disable insertion of identity columns (Id)
                context.SetAutoIncrementOnTable(kundeTableName, false);
            }
        }

        private static void SeedReservation(
            AutoReservationContext context,
            string reservationTableName)
        {
            try
            {
                // Reset the identity seed (Id's will start again from 1)
                context.ResetEntitySeed(reservationTableName);

                // Temporarily allow insertion of identity columns (Id)
                context.SetAutoIncrementOnTable(reservationTableName, true);

                // Insert test data
                context.Reservationen.AddRange(Reservationen);
                context.SaveChanges();
            }
            finally
            {
                // Disable insertion of identity columns (Id)
                context.SetAutoIncrementOnTable(reservationTableName, false);
            }
        }

        private static List<Auto> Autos =>
            new List<Auto>
            {
                new StandardAuto {Id = 1, Marke = "Fiat Punto", Tagestarif = 50},
                new MittelklasseAuto {Id = 2, Marke = "VW Golf", Tagestarif = 120},
                new LuxusklasseAuto {Id = 3, Marke = "Audi S6", Tagestarif = 180, Basistarif = 50},
                new StandardAuto {Id = 4, Marke = "Fiat 500", Tagestarif = 75},
            };

        private static List<Kunde> Kunden =>
            new List<Kunde>
            {
                new Kunde {Id = 1, Nachname = "Nass", Vorname = "Anna", Geburtsdatum = new DateTime(1981, 05, 05)},
                new Kunde {Id = 2, Nachname = "Beil", Vorname = "Timo", Geburtsdatum = new DateTime(1980, 09, 09)},
                new Kunde {Id = 3, Nachname = "Pfahl", Vorname = "Martha", Geburtsdatum = new DateTime(1990, 07, 03)},
                new Kunde {Id = 4, Nachname = "Zufall", Vorname = "Rainer", Geburtsdatum = new DateTime(1954, 11, 11)},
            };

        private static List<Reservation> Reservationen =>
            new List<Reservation>
            {
                new Reservation { ReservationsNr = 1, AutoId = 1, KundeId = 1, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
                new Reservation { ReservationsNr = 2, AutoId = 2, KundeId = 2, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
                new Reservation { ReservationsNr = 3, AutoId = 3, KundeId = 3, Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)},
                new Reservation { ReservationsNr = 4, AutoId = 2, KundeId = 1, Von = new DateTime(2020, 05, 19), Bis = new DateTime(2020, 06, 19)},
            };

        private static string GetTableName<T>(this DbContext context)
        {
            IRelationalEntityTypeAnnotations entityTypeAnnotations = context.Model
                .FindEntityType(typeof(T))
                .Relational();

            string schema = entityTypeAnnotations.Schema;
            string table = entityTypeAnnotations.TableName;

            return string.IsNullOrWhiteSpace(schema)
                ? $"[{table}]"
                : $"[{schema}].[{table}]";
        }

        private static void DeleteAllRecords(this DbContext context, string table)
        {
            string statement = $"DELETE FROM {table}"; // Must be a separate variable
            context.Database.ExecuteSqlCommand(statement);
        }

        private static void ResetEntitySeed(this DbContext context, string table)
        {
            if (context.TableHasIdentityColumn(table))
            {
                string statement = $"DBCC CHECKIDENT('{table}', RESEED, 0)"; // Must be a separate variable
                context.Database.ExecuteSqlCommand(statement);
            }
        }

        private static void SetAutoIncrementOnTable(
            this DbContext context,
            string table, bool
                isAutoIncrementOn)
        {
            if (context.TableHasIdentityColumn(table))
            {
                string statement = $"SET IDENTITY_INSERT {table} {(isAutoIncrementOn ? "ON" : "OFF")}"; // Must be a separate variable
                context.Database.ExecuteSqlCommand(statement);
            }
        }

        private static bool TableHasIdentityColumn(
            this DbContext context,
            string table)
        {
            bool hasIdentityColumn = false;
            DbCommand command = context.Database.GetDbConnection().CreateCommand();
            try
            {
                command.CommandText = $"SELECT OBJECTPROPERTY(OBJECT_ID('{table}'), 'TableHasIdentity')";
                command.CommandType = CommandType.Text;

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        hasIdentityColumn = reader.GetInt32(0) == 1;
                    }
                }
            }
            catch
            {
                hasIdentityColumn = false;
            }
            finally
            {
                command?.Dispose();
            }

            return hasIdentityColumn;
        }
    }
}