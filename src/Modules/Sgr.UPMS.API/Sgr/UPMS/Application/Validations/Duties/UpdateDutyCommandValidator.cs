/**************************************************************
 * 
 * 唯一标识：57d18e62-69e6-4882-97c7-1914fe3b9cc2
 * 命名空间：Sgr.UPMS.Application.Validations.Duties
 * 创建时间：2023/8/27 19:27:48
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Duties;

namespace Sgr.UPMS.Application.Validations.Duties
{
    public class UpdateDutyCommandValidator : AbstractValidator<UpdateDutyCommand>
    {
        public UpdateDutyCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Remarks).MaximumLength(500);
        }
    }
}
