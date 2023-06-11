using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PodkarpackiLekarz.Core.Calendars;
using PodkarpackiLekarz.Core.Users.Doctors;
using PodkarpackiLekarz.Core.Users.Patients;

namespace PodkarpackiLekarz.Infrastructure.Persistence.EntityConfigurations.Calendar
{
    public class DayEntityConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.ToTable("Days");

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.HasIndex(x => new {x.DoctorId, x.Date})
                .IsUnique(true);

            builder.HasIndex(x => x.Date)
                .IsUnique(false);

            builder.OwnsMany<Slot>(x => x.Slots, b =>
            {
                b.ToTable("Slots");
                b.Property(x => x.Id).ValueGeneratedNever();
                b.HasKey(x => x.Id);
                b.OwnsMany<Appoinment>(c => c.Appoinments, q =>
                {
                    q.ToTable("Appoinments");
                    q.Property(x => x.Id).ValueGeneratedNever();
                    q.HasKey(x => x.Id);
                    q.HasOne<Patient>().WithMany().HasForeignKey(w => w.PatientId).OnDelete(DeleteBehavior.NoAction);
                    q.Ignore(w => w.IsWaiting);
                    q.Ignore(w => w.IsCancelled);
                    q.Ignore(w => w.IsClosed);
                    q.Ignore(w => w.IsAccepted);
                    q.WithOwner().HasForeignKey("SlotId");
                });

                b.OwnsOne<Timeframe>(x => x.Timeframe, n =>
                {
                    n.Property(x => x.StartTime)
                    .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();

                    n.Property(x => x.EndTime)
                    .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
                });

                b.WithOwner().HasForeignKey("DayId");
            });

            builder.HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter() : base(
                    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime))
            {
            }
        }

        private class DateOnlyComparer : ValueComparer<DateOnly>
        {
            public DateOnlyComparer() : base(
                (d1, d2) => d1.DayNumber == d2.DayNumber,
                d => d.GetHashCode())
            {
            }
        }

        private class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
        {
            public TimeOnlyConverter() : base(
                    timeOnly => timeOnly.ToTimeSpan(),
                    timeSpan => TimeOnly.FromTimeSpan(timeSpan))
            {
            }
        }

        private class TimeOnlyComparer : ValueComparer<TimeOnly>
        {
            public TimeOnlyComparer() : base(
                (t1, t2) => t1.Ticks == t2.Ticks,
                t => t.GetHashCode())
            {
            }
        }
    }
}
