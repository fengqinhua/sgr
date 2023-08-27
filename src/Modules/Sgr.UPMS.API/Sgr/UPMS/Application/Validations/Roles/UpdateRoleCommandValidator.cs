/**************************************************************
 * 
 * 唯一标识：704c2c07-0a9e-4bf0-a423-8531f9b77c12
 * 命名空间：Sgr.UPMS.Application.Validations.Roles
 * 创建时间：2023/8/27 18:00:17
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
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(command => command.RoleName).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Remarks).MaximumLength(500);
        }
    }
}