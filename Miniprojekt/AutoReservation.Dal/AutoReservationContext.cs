using System.Configuration;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoReservation.Dal
{
    public class AutoReservationContext : DbContext
    {
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(
            new[] { new ConsoleLoggerProvider((_, logLevel) => logLevel >= LogLevel.Information, true) }
        );

        public DbSet<Auto> Autos { get; set; }

        public DbSet<Kunde> Kunden { get; set; }

        public DbSet<Reservation> Reservationen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                    .UseSqlServer(ConfigurationManager.ConnectionStrings[nameof(AutoReservationContext)]
                        .ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Auto>()
                .HasDiscriminator<int>("AutoKlasse")
                .HasValue<StandardAuto>(0)
                .HasValue<MittelklasseAuto>(1)
                .HasValue<LuxusklasseAuto>(2);
        }
    }
}