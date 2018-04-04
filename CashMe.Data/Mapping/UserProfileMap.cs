using System.Data.Entity.ModelConfiguration;
using CashMe.Core.Data;

namespace CashMe.Data.Mapping
{
   public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
       public UserProfileMap()
       {
           //key
           HasKey(t => t.Id);
           //properties           
           Property(t => t.FirstName).IsRequired().HasMaxLength(100).HasColumnType("nvarchar");
           Property(t => t.LastName).HasMaxLength(100).HasColumnType("nvarchar");
           Property(t => t.Address).HasColumnType("nvarchar");
           //table
           ToTable("UserProfiles");
       }       
    }
}
