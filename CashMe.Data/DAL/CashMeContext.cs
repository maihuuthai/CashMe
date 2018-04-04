
using CashMe.Core.Data;
using CashMe.Data.Mapping;
using CashMe.Shared.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CashMe.Data.DAL
{
    public class CashMeContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public CashMeContext() : base("name=DbConnect")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CashMeContext, CashMeInitializer>());//initial database use test data            
        }

        public static CashMeContext Create()
        {
            return new CashMeContext();
        }

        //DbContext
        public DbSet<Category> Category { get; set; }
        public DbSet<Linked_Site> Linked_Site { get; set; }
        public DbSet<Percent> Percent { get; set; }
        public DbSet<History_Checkout> History_Checkout { get; set; }
        public DbSet<Main_Cashback> Main_Cashback { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<GroupSite> GroupSite { get; set; }
        public DbSet<Message> Message { get; set; }

        //Auth

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Configurations.Add(new UserMap());    
            modelBuilder.Configurations.Add(new Display_MenuAdminMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
