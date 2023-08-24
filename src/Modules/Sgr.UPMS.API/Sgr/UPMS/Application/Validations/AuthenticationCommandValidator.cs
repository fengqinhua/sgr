/**************************************************************
 * 
 * 唯一标识：ed21eb7d-449a-468f-8c95-49685b88fbdd
 * 命名空间：Sgr.UPMS.Application.Validations
 * 创建时间：2023/8/24 18:58:29
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
    public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
    {
        public AuthenticationCommandValidator()
        {
            RuleFor(command => command.UsciCode).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);

            RuleFor(command => command.OrgTypeCode).NotEmpty().MaximumLength(50);
            RuleFor(command => command.AreaCode).NotEmpty().MaximumLength(50);

            RuleFor(command => command.Leader).MaximumLength(50);
            RuleFor(command => command.Phone).MaximumLength(30);
            RuleFor(command => command.Email).MaximumLength(100);
            RuleFor(command => command.Address).MaximumLength(200);
        }
    }
}
