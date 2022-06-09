using System;
using System.Collections.Generic;
using order_svc.Models;

namespace order_svc.Req
{
    public partial class SoMaster
    {
        public long Sysno { get; set; }
        public string? SoId { get; set; }
        /// <summary>
        /// 下单用户号
        /// </summary>
        public long? BuyerUserSysno { get; set; }
        /// <summary>
        /// 卖家公司编号
        /// </summary>
        public string? SellerCompanyCode { get; set; }
        public long ReceiveDivisionSysno { get; set; }
        public string? ReceiveAddress { get; set; }
        public string? ReceiveZip { get; set; }
        public string? ReceiveContact { get; set; }
        public string? ReceiveContactPhone { get; set; }
        public long? StockSysno { get; set; }
        /// <summary>
        /// 支付方式：1，支付宝，2，微信
        /// </summary>
        public sbyte? PaymentType { get; set; }
        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal? SoAmt { get; set; }
        /// <summary>
        /// 10,创建成功，待支付；30；支付成功，待发货；50；发货成功，待收货；70，确认收货，已完成；90，下单失败；100已作废
        /// </summary>
        public sbyte? Status { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public string? Appid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Memo { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? GmtCreate { get; set; }
        public string? ModifyUser { get; set; }
        public DateTime? GmtModified { get; set; }

        public IList<SoItem>? SoItems { get; set; }
    }
}
