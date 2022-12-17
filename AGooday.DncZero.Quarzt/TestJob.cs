using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGooday.DncZero.Quarzt
{
    public class TestJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Task task = null;
            try
            {
                string fileName = "printlog.txt";
                StreamWriter writer = new StreamWriter(fileName, true);
                task = writer.WriteLineAsync(string.Format("{0},测试", DateTime.Now.ToLongTimeString()));
                writer.Close();
                writer.Dispose();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteLog(ex.Message.ToString(), ex);
            }
            return task;
        }
    }
}
