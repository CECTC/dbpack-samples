using System;
using System.Collections.Generic;

namespace order_svc.Models
{
    /// <summary>
    /// 订单明细表
    /// </summary>
    public partial class SoItem
    {
        public long Sysno { get; set; }
        public long? SoSysno { get; set; }
        public long? ProductSysno { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal? CostPrice { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public decimal? OriginalPrice { get; set; }
        /// <summary>
        /// 成交价
        /// </summary>
        public decimal? DealPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Quantity { get; set; }
    }
}
