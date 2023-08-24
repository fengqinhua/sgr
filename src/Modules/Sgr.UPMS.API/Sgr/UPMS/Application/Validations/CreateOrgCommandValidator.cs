/**************************************************************
 * 
 * 唯一标识：5c35b852-f77d-4161-80c0-b3b3a2a97676
 * 命名空间：Sgr.UPMS.Application.Validations
 * 创建时间：2023/8/24 6:32:21
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Microsoft.Extensions.Logging;
using Sgr.UPMS.Application.Commands.Organizations;

namespace Sgr.UPMS.Application.Validations
{
    public class CreateOrgCommandValidator : AbstractValidator<CreateOrgCommand>
    {
        public CreateOrgCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);
            RuleFor(command => command.StaffSizeCode).NotEmpty().MaximumLength(50);
            RuleFor(command => command.Remarks).NotEmpty().MaximumLength(500);

            RuleFor(command => command.OrgTypeCode).NotEmpty().MaximumLength(50);
            RuleFor(command => command.AreaCode).NotEmpty().MaximumLength(50);

            RuleFor(command => command.Leader).MaximumLength(50);
            RuleFor(command => command.Phone).MaximumLength(30);
            RuleFor(command => command.Email).MaximumLength(100);
            RuleFor(command => command.Address).MaximumLength(200);

            RuleFor(command => command.AdminName).NotEmpty().MaximumLength(200);
            RuleFor(command => command.AdminPassword).NotEmpty().MaximumLength(100);
        }
    }
}
