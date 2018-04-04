using System;
namespace CashMe.Core.Data
{
    public class Main_Cashback
    {
        public int Id { get; set; }
        public int Linked_SiteId { get; set; }
        public int CategoriesId { get; set; }
        public int PercentId { get; set; }
        public DateTime CreateDate { get; set; }
        public int Flag { get; set; }
    }
}
