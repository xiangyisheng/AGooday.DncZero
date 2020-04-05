using AGooday.DncZero.Domain.Validations.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Commands.Users
{
    /// <summary>
    /// 注册一个删除 Users 命令
    /// 基础抽象Users命令模型
    /// </summary>
    public class RemoveUsersCommand : UsersCommand
    {
        public RemoveUsersCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveUsersCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
