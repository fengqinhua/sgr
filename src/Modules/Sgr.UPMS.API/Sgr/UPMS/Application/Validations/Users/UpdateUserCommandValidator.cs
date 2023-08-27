/**************************************************************
 * 
 * 唯一标识：a8bdd5e7-c4bc-457c-b664-c90748d4e008
 * 命名空间：Sgr.UPMS.Application.Validations.Users
 * 创建时间：2023/8/27 16:45:00
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
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.UserName).MaximumLength(200);
        }
    }
}
