﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlogProject.CORE.Map;
using MyBlogProject.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.MODEL.Maps
{
    public class UserMap : CoreMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.ImageURL).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.EmailAddress).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired(true);
            builder.Property(x => x.LastLogin).IsRequired(false);
            builder.Property(x => x.LastIPAddress).HasMaxLength(20).IsRequired(false);
            
            
            //Silmiyoruz
            base.Configure(builder);
        }
    }
}
