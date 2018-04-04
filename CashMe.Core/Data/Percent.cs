using System;
namespace CashMe.Core.Data
{
    public class Percent
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Flag { get; set; }
    }
}
