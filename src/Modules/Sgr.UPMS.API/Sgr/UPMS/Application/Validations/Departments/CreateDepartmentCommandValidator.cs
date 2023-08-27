/**************************************************************
 * 
 * 唯一标识：f04af98a-d74e-407e-8dfe-9ca8bd4a2961
 * 命名空间：Sgr.UPMS.Application.Validations.Departments
 * 创建时间：2023/8/27 20:37:10
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Departments;
using Sgr.UPMS.Application.Commands.Duties;

namespace Sgr.UPMS.Application.Validations.Departments
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Remarks).MaximumLength(500);

            RuleFor(command => command.Leader).MaximumLength(50);
            RuleFor(command => command.Phone).MaximumLength(50);
            RuleFor(command => command.Email).MaximumLength(50);
        }
    }
}