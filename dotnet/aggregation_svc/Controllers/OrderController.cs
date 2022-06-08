using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aggregation_svc.Req;
using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace order_svc.Controllers;

[ApiController]
[Route("v1/order")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;

    private readonly HttpClient _httpClient;

    private readonly string createSoUrl = "http://order-svc:3001/createSo";
    private readonly string updateInventoryUrl = "http://product-svc:3002/allocateInventory";

    public OrderController(ILogger<OrderController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create()
    {
        var xid = Request.Headers["x-dbpack-xid"];
        _logger.LogInformation("xid is: {xid}", xid);

        var soMasters = new List<SoMaster>()
        {
            new SoMaster
            {
                BuyerUserSysno = 10001,
                SellerCompanyCode = "SC001",
                ReceiveDivisionSysno = 110105,
                ReceiveAddress = "beijing",
                ReceiveZip = "000001",
                ReceiveContact = "scott",
                ReceiveContactPhone = "18728828296",
                StockSysno = 1,
                PaymentType = 1,
                SoAmt = 6999 * 2,
                Status = 10,
                Appid = "dk-order",
                SoItems = new List<SoItem>()
                {
                    new SoItem
                    {
                        ProductSysno = 1,
                        ProductName = "apple iphone 13",
                        CostPrice = 6799,
                        OriginalPrice = 6799,
                        DealPrice = 6999,
                        Quantity = 2,
                    }
                },
            }
        };

        var allocateInventoryReqs = new List<AllocateInventoryReq>()
        {
            new AllocateInventoryReq { ProductSysNo = 1, Qty = 2, }
        };

        var soReq = new StringContent(
            JsonConvert.SerializeObject(soMasters),
            Encoding.UTF8,
            Application.Json
        );

        var ivtReq = new StringContent(
            JsonConvert.SerializeObject(allocateInventoryReqs),
            Encoding.UTF8,
            Application.Json
        );

        soReq.Headers.Add("xid", new List<string>() { xid });
        using var httpResponseMessage = await _httpClient.PostAsync(createSoUrl, soReq);
        httpResponseMessage.EnsureSuccessStatusCode();

        ivtReq.Headers.Add("xid", new List<string>() { xid });
        using var httpResponseMessage2 = await _httpClient.PostAsync(updateInventoryUrl, ivtReq);
        httpResponseMessage2.EnsureSuccessStatusCode();

        return Ok(new {message="success", success=true});
    }

    [HttpPost("create2")]
    public async Task<IActionResult> Create2()
    {
        var xid = Request.Headers["x-dbpack-xid"];
        _logger.LogInformation("xid is: {xid}", xid);

        var soMasters = new List<SoMaster>()
        {
            new SoMaster
            {
                BuyerUserSysno = 10001,
                SellerCompanyCode = "SC001",
                ReceiveDivisionSysno = 110105,
                ReceiveAddress = "beijing",
                ReceiveZip = "000001",
                ReceiveContact = "scott",
                ReceiveContactPhone = "18728828296",
                StockSysno = 1,
                PaymentType = 1,
                SoAmt = 6999 * 2,
                Status = 10,
                Appid = "dk-order",
                SoItems = new List<SoItem>()
                {
                    new SoItem
                    {
                        ProductSysno = 1,
                        ProductName = "apple iphone 13",
                        CostPrice = 6799,
                        OriginalPrice = 6799,
                        DealPrice = 6999,
                        Quantity = 2,
                    }
                },
            }
        };

        var allocateInventoryReqs = new List<AllocateInventoryReq>()
        {
            new AllocateInventoryReq { ProductSysNo = 1, Qty = 2, }
        };

        var soReq = new StringContent(
            JsonConvert.SerializeObject(soMasters),
            Encoding.UTF8,
            Application.Json
        );

        var ivtReq = new StringContent(
            JsonConvert.SerializeObject(allocateInventoryReqs),
            Encoding.UTF8,
            Application.Json
        );

        soReq.Headers.Add("xid", new List<string>() { xid });
        using var httpResponseMessage = await _httpClient.PostAsync(createSoUrl, soReq);
        httpResponseMessage.EnsureSuccessStatusCode();

        ivtReq.Headers.Add("xid", new List<string>() { xid });
        using var httpResponseMessage2 = await _httpClient.PostAsync(updateInventoryUrl, ivtReq);
        httpResponseMessage2.EnsureSuccessStatusCode();

        return BadRequest();
    }
}
