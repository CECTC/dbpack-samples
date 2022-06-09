using System;

namespace product_svc.Req
{
    public partial class AllocateInventoryReq
    {
        public long ProductSysNo { get; set; }
        public int Qty { get; set; }
    }
}
