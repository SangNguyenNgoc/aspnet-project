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
        var result = await _billService.CreateBill(billCreate, User);
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

}