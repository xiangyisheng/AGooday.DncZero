using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGooday.DncZero.Quarzt
{
    public class PushJob : IJob
    {
        #region 任务执行
        Task IJob.Execute(IJobExecutionContext context)
        {
            Task task = null;
            try
            {
                //LogHelper.Log($"推送成功=>这里就放你想做的事务", "TaskScheduling");
            }
            catch (Exception ex)
            {
                //LogHelper.Log($"推送失败=>{ex.Message.ToString()}", "TaskScheduling");
            }
            return task;
        }
        #endregion
    }
}
