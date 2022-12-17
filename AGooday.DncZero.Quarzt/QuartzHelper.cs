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
{   /// <summary>
    /// 任务调度帮助类
    /// </summary>
    public class QuartzHelper
    {
        #region 公共变量
        /// <summary>
        /// 任务调度器
        /// </summary>
        public readonly IScheduler Scheduler;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QuartzHelper()
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
            Scheduler = schedulerFactory.GetScheduler().Result;
            //1、开启调度
            Start();

            //室内环境Http推送功能
            AddJobAndTrigger();
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// 开启任务
        /// </summary>
        public void Start()
        {
            Scheduler.Start();
        }

        /// <summary>
        /// 关闭任务
        /// </summary>
        public void Stop()
        {
            //true：表示该Sheduler关闭之前需要等现在所有正在运行的工作完成才能关闭
            //false：表示直接关闭
            Scheduler.Shutdown(true);
        }

        /// <summary>
        /// 暂停调度
        /// </summary>
        public void Pause()
        {
            Scheduler.PauseAll();
        }

        /// <summary>
        /// 继续调度
        /// </summary>
        public void Continue()
        {
            Scheduler.ResumeAll();
        }
        #endregion

        #region 不同时间方式执行任务
        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：秒)</param>
        public async Task<bool> ExecuteByInterval<T>(int seconds) where T : IJob
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create<T>().Build();

            //3、创建触发器
            ITrigger trigger = TriggerBuilder.Create()
           .StartNow()
           .WithSimpleSchedule(
                x => x.WithIntervalInSeconds(seconds)
                //x.WithIntervalInMinutes(1)
                .RepeatForever())
           .Build();

            //4、将任务加入到任务池
            await Scheduler.ScheduleJob(job, trigger);
            return true;
        }
        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public async Task<bool> ExecuteByCron<T>(string cronExpression) where T : IJob
        {
            //2、创建工作任务
            IJobDetail job = JobBuilder.Create<T>().Build();
            //3、创建触发器
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
            .StartNow()
            .WithCronSchedule(cronExpression)
            .Build();
            //4、将任务加入到任务池
            await Scheduler.ScheduleJob(job, trigger);
            return true;
        }
        #endregion

        #region 添加任务和触发器
        /// <summary>
        /// 添加任务和触发器
        /// </summary>
        public void AddJobAndTrigger()
        {
        }
        #endregion
    }
}
