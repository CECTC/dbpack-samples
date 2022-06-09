using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using order_svc.Data;
using order_svc.Models;
using order_svc.Req;
using Snowflake.Core;

namespace order_svc.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly OrderContext _context;
    private readonly string insertSoMaster =
        @"INSERT /*+ XID('{0}') */ INTO order.so_master (sysno, so_id, buyer_user_sysno, seller_company_code, 
		receive_division_sysno, receive_address, receive_zip, receive_contact, receive_contact_phone, stock_sysno, 
        payment_type, so_amt, status, order_date, appid, memo) VALUES (@p0, @p1, @p2, @p3, @p4,
        @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, now(), @p13, @p14)";

    private readonly string insertSoItem =
        @"INSERT /*+ XID('{0}') */ INTO order.so_item(sysno, so_sysno, product_sysno, product_name, cost_price, 
		original_price, deal_price, quantity) VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7)";

    private readonly IdWorker worker = new IdWorker(1, 1, 0);

    public OrderController(ILogger<OrderController> logger, OrderContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("createSo")]
    public async Task<IActionResult> PutOrders(IList<order_svc.Req.SoMaster> soMasters)
    {
        var xid = Request.Headers["xid"];
        _logger.LogInformation("xid is: {xid}", xid);
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            foreach (order_svc.Req.SoMaster soMaster in soMasters)
            {
                var soid = worker.NextId();
                _context.Database.ExecuteSqlRaw(
                    String.Format(insertSoMaster, xid),
                    soid,
                    soid,
                    soMaster.BuyerUserSysno,
                    soMaster.SellerCompanyCode,
                    soMaster.ReceiveDivisionSysno,
                    soMaster.ReceiveAddress,
                    soMaster.ReceiveZip,
                    soMaster.ReceiveContact,
                    soMaster.ReceiveContactPhone,
                    soMaster.StockSysno,
                    soMaster.PaymentType,
                    soMaster.SoAmt,
                    soMaster.Status,
                    soMaster.Appid,
                    soMaster.Memo
                );
                if (soMaster.SoItems != null)
                {
                    foreach (SoItem soItem in soMaster.SoItems)
                    {
                        soItem.Sysno = worker.NextId();
                        soItem.SoSysno = soid;
                        _context.Database.ExecuteSqlRaw(
                            String.Format(insertSoItem, xid),
                            soItem.Sysno,
                            soItem.SoSysno,
                            soItem.ProductSysno,
                            soItem.ProductName,
                            soItem.CostPrice,
                            soItem.OriginalPrice,
                            soItem.DealPrice,
                            soItem.Quantity
                        );
                    }
                }
            }
            _context.SaveChanges();
            await transaction.CommitAsync();
            return Ok();
        }
        catch (Exception)
        {
            // TODO: Handle failure
            return BadRequest();
        }
    }
}
