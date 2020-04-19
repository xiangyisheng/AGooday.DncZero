using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AGooday.DncZero.Application.ViewModels
{
    public class GroupsViewModel
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称是必需的")]
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public long? Sort { get; set; }
    }
}
