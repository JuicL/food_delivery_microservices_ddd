﻿
namespace FoodDelibery.IntegrationEventLogEF;

public static class IntegrationLogExtensions
{
    public static void UseIntegrationEventLogs(this ModelBuilder builder)
    {
        builder.Entity<IntegrationEventLogEntry>(builder =>
        {
            builder.ToTable("IntegrationEventLog", "dbo");

            builder.HasKey(e => e.EventId);
        });
    }
}
