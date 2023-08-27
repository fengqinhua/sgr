/**************************************************************
 * 
 * 唯一标识：9d8599b4-4b56-4725-8ffc-d72f048fd997
 * 命名空间：Sgr.UPMS.Application.Validations.Users
 * 创建时间：2023/8/27 16:41:58
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Users;
using Sgr.Utilities;

namespace Sgr.UPMS.Application.Validations.Users
{
    public class ModifyUserCommandValidator : AbstractValidator<ModifyUserCommand>
    {
        public ModifyUserCommandValidator()
        {
            RuleFor(command => command.UserName).MaximumLength(200);

            RuleFor(command => command.UserPhone).MaximumLength(30);
            RuleFor(command => command.UserEmail).MaximumLength(100);
            RuleFor(command => command.QQ).MaximumLength(20);
            RuleFor(command => command.Wechat).MaximumLength(100);
        }
    }
}
