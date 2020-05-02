using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AGooday.DncZero.Application.ViewModels
{
    public class RegisterViewModel
    {
        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称"), Required, MinLength(1), MaxLength(20)]
        public string NickName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "账号"), Required, MinLength(4), MaxLength(20)]
        [RegularExpression(@"^([\w-\.]+)@([\w-\.]+)(\.[a-zA-Z0-9]+)$", ErrorMessage = "登录账号必须有效邮箱地址")]
        public string Identifier { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Required, MinLength(6), MaxLength(12)]
        public string Credential { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码")]
        [Required, MinLength(6), MaxLength(12)]

        public string VerifyPassword { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; }
    }
}
