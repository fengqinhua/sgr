/**************************************************************
 * 
 * 唯一标识：85d71b48-d6c2-4306-8cf9-ba82929f0323
 * 命名空间：Sgr.UPMS.Application.Validations
 * 创建时间：2023/8/24 19:05:44
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Organizations;

namespace Sgr.UPMS.Application.Validations
{
    public class RegisterOrgCommandValidator : AbstractValidator<RegisterOrgCommand>
    {
        public RegisterOrgCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);
            RuleFor(command => command.StaffSizeCode).NotEmpty().MaximumLength(50);
            RuleFor(command => command.Remarks).NotEmpty().MaximumLength(500);

            RuleFor(command => command.Phone).MaximumLength(30);
            RuleFor(command => command.Email).MaximumLength(100);

            RuleFor(command => command.AdminName).NotEmpty().MaximumLength(200);
            RuleFor(command => command.AdminPassword).NotEmpty().MaximumLength(100);

        }
    }
}