using System;
namespace CashMe.Core.Data
{
    public class GroupSite
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Flag { get; set; }
    }
}
