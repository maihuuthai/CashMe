using System;
namespace CashMe.Core.Data
{
    public class Linked_Site
    {
        public int Id { get; set; }
        public int GroupSiteId { get; set; }
        public string Logo { get; set; }
        public string SiteName { get; set; }
        public string LinkAffiliate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Flag { get; set; }
    }
}
