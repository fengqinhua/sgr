/**************************************************************
 * 
 * 唯一标识：3fe0a277-ae79-4b6b-b2fd-ed182906e177
 * 命名空间：Sgr.UPMS.Application.Validations
 * 创建时间：2023/8/24 19:28:38
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
    public class UpdateOrgCommandValidator : AbstractValidator<UpdateOrgCommand>
    {
        public UpdateOrgCommandValidator()
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
        }
    }
}
