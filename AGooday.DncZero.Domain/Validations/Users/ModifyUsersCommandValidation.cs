﻿using AGooday.DncZero.Domain.Commands.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGooday.DncZero.Domain.Validations.Users
{
    /// <summary>
    /// 添加 Users 命令模型验证
    /// 继承 UsersValidation 基类
    /// </summary>
    public class ModifyUsersCommandValidation : AbstractValidator<ModifyUsersCommand>
    {
        public ModifyUsersCommandValidation()
        {
            ValidateId();
            ValidateName();//验证姓名
            ValidateBirthDate();//验证年龄
            ValidateEmail();//验证邮箱
            ValidatePhone();//验证手机号
            //可以自定义增加新的验证
        }

        //受保护方法，验证Name
        protected void ValidateName()
        {
            //定义规则，c 就是当前 StudentCommand 类
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("姓名不能为空")//判断不能为空，如果为空则显示Message
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");//定义 Name 的长度
        }

        //验证年龄
        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)//Must 表示必须满足某一个条件，参数是一个bool类型的方法，更像是一个委托事件
                .WithMessage("学生应该14岁以上！");
        }

        //验证邮箱
        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
        //验证手机号
        protected void ValidatePhone()
        {
            RuleFor(c => c.Phone)
                .NotEmpty()
                .Must(HavePhone)
                .WithMessage("手机号应该为11位！");
        }

        //验证Guid
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }

        // 表达式
        protected static bool HaveMinimumAge(DateTime? birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-14);
        }
        // 表达式
        protected static bool HavePhone(string phone)
        {
            return phone?.Length == 11;
        }
    }
}
