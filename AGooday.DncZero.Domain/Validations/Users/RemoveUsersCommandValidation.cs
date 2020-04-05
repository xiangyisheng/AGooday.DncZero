using AGooday.DncZero.Domain.Commands.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Validations.Users
{
    /// <summary>
    /// 删除 Users 命令模型验证
    /// 继承 UsersValidation 基类
    /// </summary>
    public class RemoveUsersCommandValidation : UsersValidation<RemoveUsersCommand>
    {
        public RemoveUsersCommandValidation()
        {
            ValidateId();
        }
    }
}
