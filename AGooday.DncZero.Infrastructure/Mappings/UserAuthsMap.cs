﻿using AGooday.DncZero.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Infrastructure.Mappings
{
    /// <summary>
    /// 授权map类
    /// </summary>
    public class UserAuthsMap : IEntityTypeConfiguration<UserAuths>
    {
        /// <summary>
        /// 实体属性配置
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserAuths> builder)
        {
            builder.HasOne(e => e.User).WithMany(c => c.UserAuths)
                .HasForeignKey(c => c.UserId).HasConstraintName("FK_UserAuths_Users_UserId")
                ;
        }
    }
}
