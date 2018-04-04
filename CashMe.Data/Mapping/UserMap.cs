using System.Data.Entity.ModelConfiguration;
using CashMe.Core.Data;

namespace CashMe.Data.Mapping
{
   public class UserMap :EntityTypeConfiguration<User> 
    {
       public UserMap()
       {
           //key
           HasKey(t => t.Id);
           //properties
           Property(t => t.UserName).IsRequired();
            Property(t => t.Password).IsRequired();
            //table
            ToTable("User");
       }
    }
}
