using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> CreateBill([FromBody] BillCreate billCreate)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) 
                     ?? throw new UnauthorizedAccessException("Unauthorized");
        var result = await _billService.CreateBill(billCreate, userId);
        return Ok(result);
    }
    
    [HttpGet("payment")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> HandlePayment(
        [FromQuery(Name = "vnp_Amount")] string amount,
        [FromQuery(Name = "vnp_BankCode")] string bankCode,
        [FromQuery(Name = "vnp_BankTranNo")] string bankTranNo,
        [FromQuery(Name = "vnp_CardType")] string cardType,
        [FromQuery(Name = "vnp_OrderInfo")] string orderInfo,
        [FromQuery(Name = "vnp_PayDate")] string payDate,
        [FromQuery(Name = "vnp_ResponseCode")] string responseCode,
        [FromQuery(Name = "vnp_TmnCode")] string tmnCode,
        [FromQuery(Name = "vnp_TransactionNo")] string transactionNo,
        [FromQuery(Name = "vnp_TransactionStatus")] string transactionStatus,
        [FromQuery(Name = "vnp_TxnRef")] string txnRef,
        [FromQuery(Name = "vnp_SecureHash")] string secureHash)
    {
        var result = await _billService.Payment(txnRef, responseCode, transactionStatus, payDate);
        return Ok(result);
    }

    [HttpGet("curr-user")]
    [Authorize]
    public async Task<IActionResult> GetBillsByCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) 
                     ?? throw new UnauthorizedAccessException("Unauthorized");
        var result = await _billService.GetBillsByUser(userId);
        return Ok(result);
    }
    
    
    [HttpGet("curr-user/{billId}")]
    [Authorize]
    public async Task<IActionResult> GetBillDetailByUser(string billId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) 
                     ?? throw new UnauthorizedAccessException("Unauthorized");
        var result = await _billService.GetBillDetailByUser(billId, userId);
        return Ok(result);
    }
    
    
    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetBillsByUser(string userId)
    {
        var result = await _billService.GetBillsByUser(userId);
        return Ok(result);
    }
    
    
    [HttpGet("{billId}")]
    [Authorize]
    public async Task<IActionResult> GetBillDetailByAdmin(string billId)
    {
        var result = await _billService.GetBillDetailByAdmin(billId);
        return Ok(result);
    }

}