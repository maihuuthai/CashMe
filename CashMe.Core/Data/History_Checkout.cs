using System;
namespace CashMe.Core.Data
{
    public class History_Checkout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Main_CaskbackId { get; set; }
        public float PriceMain { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int Status { get; set; }
        public int Flag { get; set; }
    }
}
