using System;

namespace CashMe.Service.Models
{
    public class VoucherModel
    {
        public int Id { get; set; }
        public int Linked_SiteId { get; set; }
        public string SiteName { get; set; }
        public string LinkAffiliate { get; set; }
        public string VoucherName { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Logo { get; set; }
        public int Flag { get; set; }

    }
}