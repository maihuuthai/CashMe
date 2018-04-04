using System;
namespace CashMe.Core.Data
{
    public class Voucher
    {
        public int Id { get; set; }
        public int Linked_SiteId { get; set; }
        public string VoucherName { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Flag { get; set; }
    }
}
