using System;

namespace CashMe.Service.Models
{
    public class CashbackModel
    {
        public int Id { get; set; }
        public int Linked_SiteId { get; set; }
        public string SiteName { get; set; }
        public string LinkAffiliate { get; set; }
        public int CategoriesId { get; set; }
        public string CategoriesName { get; set; }
        public int PercentId { get; set; }
        public float Value { get; set; }
        public DateTime CreateDate { get; set; }
        public int Flag { get; set; }
        public string Logo { get; set; }
        public int GroupSiteId { get; set; }

    }
}