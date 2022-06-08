using System;
using System.Collections.Generic;

namespace product_svc.Models
{
    /// <summary>
    /// 商品SKU
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// 主键
        /// </summary>
        public ulong Sysno { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string ProductName { get; set; } = null!;
        public string ProductTitle { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductDesc { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductDescLong { get; set; } = null!;
        public string? DefaultImageSrc { get; set; }
        public long? C3Sysno { get; set; }
        public string? Barcode { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public float? Weight { get; set; }
        public long? MerchantSysno { get; set; }
        public string? MerchantProductid { get; set; }
        /// <summary>
        /// 1，待上架；2，上架；3，下架；4，售罄下架；5，违规下架
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime GmtCreate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; } = null!;
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUser { get; set; } = null!;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime GmtModified { get; set; }
    }
}
