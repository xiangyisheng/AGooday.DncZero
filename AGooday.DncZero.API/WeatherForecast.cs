using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGooday.DncZero.API
{
    /// <summary>
    /// ����Ԥ��
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Required]
        [DefaultValue(typeof(DateTime),"2020-05-05")]
        public DateTime Date { get; set; }

        /// <summary>
        /// ���϶�
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// ���϶�
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// ��������
        /// </summary>
        public string Summary { get; set; }
    }
}
