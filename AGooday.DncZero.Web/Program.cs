using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AGooday.DncZero.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    // ���� ClearProviders �Դ���������ɾ������ ILoggerProvider ʵ��
                    logging.ClearProviders();
                    // ͨ������־����Ӧ��������ָ�����������ڴ�����ָ����
                    logging.AddFilter("Microsoft", LogLevel.Warning);
                    // ��ӿ���̨��־��¼�ṩ����
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
