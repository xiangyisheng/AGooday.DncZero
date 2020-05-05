using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGooday.DncZero.API
{
    /// <summary>
    /// 天气预报
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期时间
        /// </summary>
        [Required]
        [DefaultValue(typeof(DateTime),"2020-05-05")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 摄氏度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 华氏度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 综上所述
        /// </summary>
        public string Summary { get; set; }
    }
}
