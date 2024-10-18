﻿using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using aspdotnet_project.App.Bill.Dtos;
using aspdotnet_project.App.Bill.Repositories;
using aspdotnet_project.App.Cinema.Entities;
using aspdotnet_project.App.Cinema.Repositories;
using aspdotnet_project.App.Show.Entities;
using aspdotnet_project.App.Show.Repositories;
using aspdotnet_project.Configurations;
using Microsoft.Extensions.Options;

namespace aspdotnet_project.App.Bill.Services;

public class BillService : IBillService
{
    private readonly IBillRepository _billRepository;
    private readonly IBillStatusRepository _billStatusRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly ITicketRepository _ticketRepository;
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly VnPayConfig _vnPayConfig;


    public BillService(IHttpContextAccessor httpContextAccessor, IOptions<VnPayConfig> vnPayConfig,
        IBillRepository billRepository, IBillStatusRepository billStatusRepository,
        IShowtimeRepository showtimeRepository, ISeatRepository seatRepository, ITicketRepository ticketRepository)
    {
        _billRepository = billRepository;
        _billStatusRepository = billStatusRepository;
        _showtimeRepository = showtimeRepository;
        _seatRepository = seatRepository;
        _ticketRepository = ticketRepository;
        
        _httpContextAccessor = httpContextAccessor;
        _vnPayConfig = vnPayConfig.Value;
    }

    public async Task<string> CreateBill(BillCreate billCreate)
    {
        const int cost = 10;
        const string id = "123";
        var paymentUrl = await GeneratePaymentUrl(cost, id);
        return paymentUrl;
    }

    public async Task CheckSeatInHall(List<long> seatIds, Hall hall)
    {
        throw new NotImplementedException();
    }

    public async Task CheckSeatAreReserved(List<long> seatIds, string showId)
    {
        throw new NotImplementedException();
    }

    public ICollection<Ticket> CreateTicket(Show.Entities.Show show, List<Seat> seats, string showId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Payment(string billId, string responseCode, string transactionStatus, string paymentAt)
    {
        throw new NotImplementedException();
    }

    public Task<string> GeneratePaymentUrl(long cost, string id)
    {
        var vnpParams = InitParams(cost, id);

        var fieldNames = vnpParams.Keys.ToList();
        fieldNames.Sort();

        var hashData = new StringBuilder();
        var query = new StringBuilder();
        foreach (var fieldName in fieldNames)
        {
            var fieldValue = vnpParams[fieldName];
            if (string.IsNullOrEmpty(fieldValue)) continue;
            // Build hash data
            hashData.Append(fieldName);
            hashData.Append('=');
            hashData.Append(HttpUtility.UrlEncode(fieldValue, Encoding.ASCII));

            // Build query
            query.Append(HttpUtility.UrlEncode(fieldName, Encoding.ASCII));
            query.Append('=');
            query.Append(HttpUtility.UrlEncode(fieldValue, Encoding.ASCII));

            if (fieldName == fieldNames.Last()) continue;
            query.Append('&');
            hashData.Append('&');
        }

        var queryUrl = query.ToString();
        var vnpSecureHash = HmacSha512(_vnPayConfig.VnpayKey, hashData.ToString());
        queryUrl += "&vnp_SecureHash=" + vnpSecureHash;
        return Task.FromResult(_vnPayConfig.VnpayUrl + "?" + queryUrl);
    }

    private string GetIpAddress()
    {
        string ipAddress;
        try
        {
            ipAddress = _httpContextAccessor.HttpContext!.Request.Headers["X-FORWARDED-FOR"]!;
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString()!;
        }
        catch (System.Exception e)
        {
            ipAddress = "Invalid IP:" + e.Message;
        }

        return ipAddress!;
    }

    private Dictionary<string, string> InitParams(long cost, string id)
    {
        const string vnpVersion = "2.1.0";
        const string vnpCommand = "pay";
        const string orderType = "other";
        var amount = cost * 100L;
        var vnpIpAddr = GetIpAddress();

        var vnpParams = new Dictionary<string, string>
        {
            { "vnp_Version", vnpVersion },
            { "vnp_Command", vnpCommand },
            { "vnp_TmnCode", _vnPayConfig.TmnCode },
            { "vnp_Amount", amount.ToString() },
            { "vnp_CurrCode", "VND" },
            { "vnp_TxnRef", id },
            { "vnp_OrderInfo", "Thanh toan ve xem phim:" + id },
            { "vnp_OrderType", orderType },
            { "vnp_Locale", "vn" },
            { "vnp_ReturnUrl", _vnPayConfig.VnpayReturnUrl },
            { "vnp_IpAddr", vnpIpAddr }
        };

        var cld = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "SE Asia Standard Time");
        const string formatter = "yyyyMMddHHmmss";
        var vnpCreateDate = cld.ToString(formatter, CultureInfo.InvariantCulture);
        vnpParams["vnp_CreateDate"] = vnpCreateDate;

        var expireDate = cld.AddMinutes(_vnPayConfig.TimeOut).ToString(formatter, CultureInfo.InvariantCulture);
        vnpParams["vnp_ExpireDate"] = expireDate;

        return vnpParams;
    }

    private static string HmacSha512(string key, string data)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}