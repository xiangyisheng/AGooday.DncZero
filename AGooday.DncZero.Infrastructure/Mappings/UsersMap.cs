using AGooday.DncZero.Common.Enumerator;
using AGooday.DncZero.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Infrastructure.Mappings
{
    /// <summary>
    /// 用户map类
    /// </summary>
    public class UsersMap : IEntityTypeConfiguration<Users>
    {
        /// <summary>
        /// 实体属性配置
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            //实体名称Map
            builder.ToTable("Users");
            #region 实体属性Map
            //实体属性Map
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                ;

            builder.Property(c => c.NickName)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                //.IsRequired()//是否必须
                ;

            builder.Property(c => c.Surname);

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                //.IsRequired()
                ;

            builder.Property(c => c.RealName);

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                //.HasComputedColumnSql("(Surname+Name)")//计算列
                //.IsRequired()
                ;

            builder.Property(c => c.Phone)
                .HasColumnType("varchar(11)")
                .HasMaxLength(11)
                //.IsRequired()
                ;

            builder.Property(c => c.BirthDate);

            builder.Property(c => c.RegisterTime)
                .HasDefaultValueSql("getdate()")//默认值
                ;

            builder.Property(c => c.State)
                .HasDefaultValue(0)//默认值
                ;

            //builder.Property(c => c.Type)
            //    .IsRequired()
            //    .HasColumnType("int")
            //    .HasConversion(v => (int)v, v => (UserType)v)
            //    .HasDefaultValueSql("0")
            //    ;

            //builder.Property(c => c.IsSuperMan)
            //    .IsRequired()
            //    .HasColumnType("bit")
            //    .HasDefaultValue(true)
            //    ;

            //处理值对象配置，否则会被视为实体
            builder.OwnsOne(p => p.Address);

            //可以对值对象进行数据库重命名，还有其他的一些操作，请参考官网
            //builder.OwnsOne(
            //    o => o.Address,
            //    sa =>
            //    {
            //        sa.Property(p => p.Province).HasColumnName("Province");
            //        sa.Property(p => p.City).HasColumnName("City");
            //        sa.Property(p => p.County).HasColumnName("County");
            //        sa.Property(p => p.Street).HasColumnName("Street");
            //        sa.Property(p => p.Detailed).HasColumnName("Detailed");
            //    }
            //);


            //注意：这是EF版本的写法，Core中不能使用！！！
            //builder.Property(c => c.Address.City)
            //     .HasColumnName("City")
            //     .HasMaxLength(20);
            //builder.Property(c => c.Address.Street)
            //     .HasColumnName("Street")
            //     .HasMaxLength(20);


            //如果想忽略当前值对象，可直接 Ignore
            //builder.Ignore(c => c.Address); 

            //一对多 https://www.learnentityframeworkcore.com/configuration/one-to-many-relationship-configuration
            builder.HasMany(c => c.UserAuths).WithOne(c => c.User)
                //.OnDelete(DeleteBehavior.SetNull)//删除主体时将从属实体的外键值设置为null
                //.OnDelete(DeleteBehavior.Delete)//删除主体时将从属实体删除
                .IsRequired()
                ;
            #endregion
        }
    }
}
