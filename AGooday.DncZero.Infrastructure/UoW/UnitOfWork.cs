using AGooday.DncZero.Domain.Interfaces;
using AGooday.DncZero.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Infrastructure.UoW
{
    /// <summary>
    /// 工作单元类
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        //数据库上下文
        private readonly DncZeroDbContext _dbcontext;

        //构造函数注入
        public UnitOfWork(DncZeroDbContext context)
        {
            _dbcontext = context;
        }

        //上下文提交
        public bool Commit()
        {
            return _dbcontext.SaveChanges() > 0;
        }

        //手动回收
        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
