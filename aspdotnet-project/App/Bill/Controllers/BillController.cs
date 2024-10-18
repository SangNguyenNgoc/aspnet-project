using aspdotnet_project.App.Bill.Dtos;
using aspdotnet_project.App.Bill.Services;
using Microsoft.AspNetCore.Mvc;

namespace aspdotnet_project.App.Bill.Controllers;

[ApiController]
[Route("/api/v1/bills")]
public class BillController : ControllerBase
{
    private readonly IBillService _billService;

    public BillController(IBillService billService)
    {
        _billService = billService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBill([FromBody] BillCreate billCreate)
    {
        var result = await _billService.CreateBill(billCreate);
        return Ok(result);
    }
}