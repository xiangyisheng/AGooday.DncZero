using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Interfaces
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        //是否提交成功
        bool Commit();
    }
}
