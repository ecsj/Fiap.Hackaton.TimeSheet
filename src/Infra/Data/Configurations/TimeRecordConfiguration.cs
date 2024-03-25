using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations;

public class TimeRecordConfiguration : IEntityTypeConfiguration<TimeRecord>
{
    public void Configure(EntityTypeBuilder<TimeRecord> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.EntryTime);
        builder.Property(a => a.ExitTime);
        builder.Property(a => a.TotalHours);
        builder.Property(a => a.EmployeeId);
    }
}

public class IntervalsConfiguration : IEntityTypeConfiguration<Intervals>
{
    public void Configure(EntityTypeBuilder<Intervals> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.EntryTime);
        builder.Property(a => a.ExitTime);
        builder.Property(a => a.IntervalType);
    }
}