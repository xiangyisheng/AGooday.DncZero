using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AGooday.DncZero.Quarzt
{
    public class QuartzUtil
    {
        static readonly IScheduler _scheduler;
        static QuartzUtil()
        {
            var properties = new NameValueCollection();
            // 设置线程池
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            //设置线程池的最大线程数量
            properties["quartz.threadPool.threadCount"] = "5";
            //设置作业中每个线程的优先级
            properties["quartz.threadPool.threadPriority"] = ThreadPriority.Normal.ToString();

            // 远程输出配置
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "555";  //配置端口号
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp"; //协议类型

            //创建一个工厂
            var schedulerFactory = new StdSchedulerFactory(properties);
            //启动
            _scheduler = schedulerFactory.GetScheduler().Result;
            //1、开启调度
            _scheduler.Start();
        }
        /// 
        /// 时间间隔执行任务
        /// 
        /// 任务类，必须实现IJob接口
        /// 时间间隔(单位：秒)
        public static async Task<bool> ExecuteInterval(int seconds) //where T : IJob
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create().Build();
            // 3、创建触发器
            ITrigger trigger = TriggerBuilder.Create()
             .StartNow()
             .WithSimpleSchedule(
            x => x.WithIntervalInSeconds(seconds)
             //x.WithIntervalInMinutes(1)
             .RepeatForever())
             .Build();
            //4、将任务加入到任务池
            await _scheduler.ScheduleJob(job, trigger);
            return true;
        }

        /// 
        /// 指定时间执行任务
        /// 
        /// 任务类，必须实现IJob接口
        /// cron表达式，即指定时间点的表达式
        public static async Task<bool> ExecuteByCron(string cronExpression) //where T : IJob
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create().Build();
            //3、创建触发器
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
             .StartNow()
             .WithCronSchedule(cronExpression)
             .Build();
            //4、将任务加入到任务池
            await _scheduler.ScheduleJob(job, trigger);
            return true;
        }
    }
}
