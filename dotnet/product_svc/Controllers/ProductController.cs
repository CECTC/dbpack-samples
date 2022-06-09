using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product_svc.Data;
using product_svc.Models;
using product_svc.Req;

namespace product_svc.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductContext _context;

    private readonly string allocateInventorySql =
        @"update /*+ XID('{0}') */ product.inventory set available_qty = available_qty - @p0,
                            		allocated_qty = allocated_qty + @p1 where product_sysno = @p2 and available_qty >= @p3";

    public ProductController(ILogger<ProductController> logger, ProductContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("allocateInventory")]
    public async Task<IActionResult> AllocateInventory(
        IList<product_svc.Req.AllocateInventoryReq> reqs
    )
    {
        var xid = Request.Headers["xid"];
        _logger.LogInformation("xid is: {xid}", xid);
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            foreach (product_svc.Req.AllocateInventoryReq req in reqs)
            {
                _context.Database.ExecuteSqlRaw(
                    String.Format(allocateInventorySql, xid),
                    req.Qty,
                    req.Qty,
                    req.ProductSysNo,
                    req.Qty
                );
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
