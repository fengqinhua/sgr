/**************************************************************
 * 
 * 唯一标识：698d5fc9-560a-4751-8142-29c528a8e7fb
 * 命名空间：Sgr.UPMS.Application.Validations.Departments
 * 创建时间：2023/8/27 20:38:42
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.UPMS.Application.Commands.Departments;

namespace Sgr.UPMS.Application.Validations.Departments
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Remarks).MaximumLength(500);

            RuleFor(command => command.Leader).MaximumLength(50);
            RuleFor(command => command.Phone).MaximumLength(50);
            RuleFor(command => command.Email).MaximumLength(50);
        }
    }
}