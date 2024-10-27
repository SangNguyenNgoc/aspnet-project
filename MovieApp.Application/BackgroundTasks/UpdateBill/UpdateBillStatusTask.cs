using Microsoft.Extensions.Logging;
using MovieApp.Application.Feature.Bill.Services;
using Quartz;

namespace MovieApp.Application.BackgroundTasks.UpdateBill;

public class UpdateBillStatusTask : IJob
{
    private readonly ILogger<UpdateBillStatusTask> _logger;
    private readonly IBillService _billService;

    public UpdateBillStatusTask(ILogger<UpdateBillStatusTask> logger, IBillService billService)
    {
        _logger = logger;
        _billService = billService;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var now = DateTime.Now;
        _logger.LogInformation("Update expired bills at {Now}", now);
        await _billService.UpdateBillsExpired(now);
    }
}