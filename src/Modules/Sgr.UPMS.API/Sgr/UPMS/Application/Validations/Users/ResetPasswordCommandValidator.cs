/**************************************************************
 * 
 * 唯一标识：16800ef9-840f-473a-b9a2-ddf0756c8e19
 * 命名空间：Sgr.UPMS.Application.Validations.Users
 * 创建时间：2023/8/27 16:44:02
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
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(command => command.Password)
                .Must((pwd) => StringHelper.SgrPasswordComplexityIsCompliant(pwd))
                .WithMessage("密码长度为8-14个字符且数字、字母以及标点符号需至少包含两种，不允许有空格、中文");

        }
    }
}
