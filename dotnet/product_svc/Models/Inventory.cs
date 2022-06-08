using System;
using System.Collections.Generic;

namespace product_svc.Models
{
    /// <summary>
    /// 商品库存
    /// </summary>
    public partial class Inventory
    {
        /// <summary>
        /// 主键
        /// </summary>
        public ulong Sysno { get; set; }
        public ulong ProductSysno { get; set; }
        /// <summary>
        /// 财务库存
        /// </summary>
        public int? AccountQty { get; set; }
        /// <summary>
        /// 可用库存
        /// </summary>
        public int? AvailableQty { get; set; }
        /// <summary>
        /// 分配库存
        /// </summary>
        public int? AllocatedQty { get; set; }
        /// <summary>
        /// 调整锁定库存
        /// </summary>
        public int? AdjustLockedQty { get; set; }
    }
}
