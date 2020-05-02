using AGooday.DncZero.Domain.Models;
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
    public class UserGroupRelationMap : IEntityTypeConfiguration<UserGroupRelation>
    {
        /// <summary>
        /// 实体属性配置
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserGroupRelation> builder)
        {

            //多对多 https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
            builder.HasKey(bc => new { bc.UserId, bc.GroupId });
            builder.HasOne(bc => bc.User).WithMany(b => b.UserGroupRelation)
                .HasForeignKey(bc => bc.UserId);
            builder.HasOne(bc => bc.Group).WithMany(c => c.UserGroupRelation)
                .HasForeignKey(bc => bc.GroupId);
        }
    }
}
