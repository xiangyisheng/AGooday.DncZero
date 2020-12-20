using AGooday.DncZero.Domain.Commands.Menus;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Validations.Menus
{
    /// <summary>
    /// 定义基于 MenusCommand 的抽象基类 MenusValidation
    /// 继承 抽象类 AbstractValidator
    /// 注意需要引用 FluentValidation
    /// 注意这里的 T 是命令模型
    /// </summary>
    /// <typeparam name="T">泛型类</typeparam>
    public abstract class MenusValidation<T> : AbstractValidator<T> where T : MenusCommand
    {
        //受保护方法，验证Name
        protected void ValidateName()
        {
            //定义规则，c 就是当前 StudentCommand 类
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("名称不能为空")//判断不能为空，如果为空则显示Message
                .Length(2, 10).WithMessage("名称在2~30个字符之间");//定义 Name 的长度
        }
    }
}
