using System;
namespace CashMe.Core.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoriesName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Flag { get; set; }
    }
}
