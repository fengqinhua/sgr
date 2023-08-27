/**************************************************************
 * 
 * 唯一标识：9e5dd409-254e-409b-9ff7-d552c7381842
 * 命名空间：Sgr.UPMS.Application.Validations.Users
 * 创建时间：2023/8/27 13:04:43
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
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.LoginName).NotEmpty().MaximumLength(200);
            RuleFor(command => command.LoginPassword)
                .Must((pwd) => StringHelper.SgrPasswordComplexityIsCompliant(pwd))
                .WithMessage("密码长度为8-14个字符且数字、字母以及标点符号需至少包含两种，不允许有空格、中文");

            RuleFor(command => command.UserName).MaximumLength(200);
        }
    }
}
