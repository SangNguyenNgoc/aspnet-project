using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MovieApp.Infrastructure.VnPay;

public class VnPayService
{
    private readonly VnPayConfig _vnPayConfig;

    public VnPayService(VnPayConfig vnPayConfig)
    {
        _vnPayConfig = vnPayConfig;
    }
    
    public string GeneratePaymentUrl(long cost, string id)
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
            hashData.Append(WebUtility.UrlEncode(fieldValue));

            // Build query
            query.Append(WebUtility.UrlEncode(fieldName));
            query.Append('=');
            query.Append(WebUtility.UrlEncode(fieldValue));

            if (fieldName == fieldNames.Last()) continue;
            query.Append('&');
            hashData.Append('&');
        }

        var queryUrl = query.ToString();
        var vnpSecureHash = HmacSha512(_vnPayConfig.VnpayKey, hashData.ToString());
        queryUrl += "&vnp_SecureHash=" + vnpSecureHash;
        return _vnPayConfig.VnpayUrl + "?" + queryUrl;
    }

    private Dictionary<string, string> InitParams(long cost, string id)
    {
        const string vnpVersion = "2.1.0";
        const string vnpCommand = "pay";
        const string orderType = "other";
        var amount = cost * 100L;
        var hostName = System.Net.Dns.GetHostName();
        var vnpIpAddr = System.Net.Dns.GetHostAddresses(hostName).GetValue(0)!.ToString();

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
            { "vnp_IpAddr", vnpIpAddr! }
        };
        
        var vnpCreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        vnpParams["vnp_CreateDate"] = vnpCreateDate;

        var expireDate = DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss");
        vnpParams["vnp_ExpireDate"] = expireDate;

        return vnpParams;
    }

    private static string HmacSha512(string key, string data)
    {
        var hash = new StringBuilder();
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var inputBytes = Encoding.UTF8.GetBytes(data);
        using (var hmac = new HMACSHA512(keyBytes))
        {
            var hashValue = hmac.ComputeHash(inputBytes);
            foreach (var theByte in hashValue)
            {
                hash.Append(theByte.ToString("x2"));
            }
        }

        return hash.ToString();
    }
    
    public static string GetMessage(string responseCode, string transactionStatus)
    {
        var responseCodeMessages = GetResponseCodeMessages();
    
        if (responseCodeMessages.TryGetValue(responseCode, out var value))
        {
            return value;
        }
    
        return transactionStatus == "01" ? "Chưa thanh toán" : "Transaction Status invalid";
    }

    private static Dictionary<string, string> GetResponseCodeMessages()
    {
        var responseCodeMessages = new Dictionary<string, string>
        {
            { "09", "Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng" },
            { "10", "Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần" },
            { "11", "Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch." },
            { "12", "Thẻ/Tài khoản của khách hàng bị khóa." },
            { "24", "Khách hàng hủy giao dịch." },
            { "51", "Tài khoản của quý khách không đủ số dư để thực hiện giao dịch." },
            { "65", "Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày." },
            { "75", "Ngân hàng thanh toán đang bảo trì." },
            { "79", "KH nhập sai mật khẩu thanh toán quá số lần quy định. Xin quý khách vui lòng thực hiện lại giao dịch." },
            { "99", "Lỗi không xác định." }
        };
        return responseCodeMessages;
    }

    public int GetTimeOut()
    {
        return _vnPayConfig.TimeOut;
    }

    public string GetBillDetailUrl()
    {
        return _vnPayConfig.BillDetailUrl;
    }

}