using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AGooday.DncZero.Application.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录账号"), Required, MinLength(4), MaxLength(20)]
        //[RegularExpression("^[^_][a-zA-Z0-9_]*$", ErrorMessage = "登录账号必须是字母、数字或者下划线的组合")]
        [RegularExpression(@"^([\w-\.]+)@([\w-\.]+)(\.[a-zA-Z0-9]+)|(((\+)?86|((\+)?86)?)0?1[3458]\d{9})|([a-zA-Z0-9_]{4,16})$", ErrorMessage = "登录账号必须是字母、数字或者下划线的组合或有效手机号码或有效邮箱地址")]
        public string Identifier { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "登录密码")]
        [Required, MinLength(6), MaxLength(12)]
        public string Credential { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 登陆成功后跳转的地址
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; }
    }
}
