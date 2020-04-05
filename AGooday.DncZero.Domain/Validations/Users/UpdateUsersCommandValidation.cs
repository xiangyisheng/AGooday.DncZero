using AGooday.DncZero.Domain.Commands.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Validations.Users
{
    /// <summary>
    /// 添加 Users 命令模型验证
    /// 继承 UsersValidation 基类
    /// </summary>
    public class UpdateUsersCommandValidation : UsersValidation<UpdateUsersCommand>
    {
        public UpdateUsersCommandValidation()
        {
            ValidateId();
            ValidateName();//验证姓名
            ValidateBirthDate();//验证年龄
            ValidateEmail();//验证邮箱
            ValidatePhone();//验证手机号
            //可以自定义增加新的验证
        }
    }
}
