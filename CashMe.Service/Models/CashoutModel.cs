using System;

namespace CashMe.Service.Models
{
    public class CashoutModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Main_CashbackId { get; set; }
        public int Linked_SiteId { get; set; }
        public string SiteName { get; set; }
        public int CategoriesId { get; set; }
        public string CategoriesName { get; set; }
        public int PercentId { get; set; }
        public float Value { get; set; }
        public float PriceMain { get; set; }
        public float Cashback { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int Flag { get; set; }

    }
}