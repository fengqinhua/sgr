/**************************************************************
 * 
 * 唯一标识：3e93cbb8-d549-4637-af02-ca333f6889fc
 * 命名空间：Sgr.UPMS.Application.Validations.Roles
 * 创建时间：2023/8/27 17:57:34
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Roles;

namespace Sgr.UPMS.Application.Validations.Roles
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(command => command.RoleName).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Remarks).MaximumLength(500);
        }
    }
}
