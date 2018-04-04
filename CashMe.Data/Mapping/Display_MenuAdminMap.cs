using CashMe.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMe.Data.Mapping
{
    public class Display_MenuAdminMap : EntityTypeConfiguration<Display_MenuAdmin>
    {
        public Display_MenuAdminMap()
        {
            HasKey(aa => aa.Id);
            Property(aa => aa.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(aa => aa.Name).IsRequired().HasMaxLength(500);
            Property(aa => aa.RoleId).IsRequired();
            //Property(aa => aa.RoleName).IsRequired().HasMaxLength(50);
            Property(aa => aa.LinkedController).IsOptional().HasMaxLength(50);
            Property(aa => aa.IsOfTheWebsite).IsRequired();
            Property(aa => aa.IsOfTheAdmin).IsRequired();
            Property(aa => aa.IsActive).IsRequired();
            ToTable("Display_MenuAdmin");
        }
    }
}
