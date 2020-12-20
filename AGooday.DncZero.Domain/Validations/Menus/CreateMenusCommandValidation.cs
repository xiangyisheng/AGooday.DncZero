using AGooday.DncZero.Domain.Commands.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Validations.Menus
{
    /// <summary>
    /// 添加 Menus 命令模型验证
    /// 继承 MenusValidation 基类
    /// </summary>
    public class CreateMenusCommandValidation : MenusValidation<CreateMenusCommand>
    {
        public CreateMenusCommandValidation()
        {
            ValidateName();//验证姓名
        }
    }
}
