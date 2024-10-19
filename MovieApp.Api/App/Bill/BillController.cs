using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Application.Feature.Bill.Dtos;
using MovieApp.Application.Feature.Bill.Services;

namespace MovieApp.Api.App.Bill;

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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _billService.CreateBill(billCreate, User);
        return Ok(result);
    }
}