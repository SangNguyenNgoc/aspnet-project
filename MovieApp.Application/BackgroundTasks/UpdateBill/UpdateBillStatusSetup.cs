using Microsoft.Extensions.Options;
using Quartz;

namespace MovieApp.Application.BackgroundTasks.UpdateBill;

public class UpdateBillStatusSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(UpdateBillStatusTask));
        options
            .AddJob<UpdateBillStatusTask>(builder => builder.WithIdentity(jobKey))
            .AddTrigger(triggerBuilder =>
            {
                triggerBuilder
                    .ForJob(jobKey)
                    .WithSimpleSchedule(scheduleBuilder =>
                    {
                        scheduleBuilder.WithIntervalInMinutes(5).RepeatForever();
                    });
            });
    }
}