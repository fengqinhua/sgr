/**************************************************************
 * 
 * 唯一标识：25731155-38d5-47a1-82fd-aa4e576220d2
 * 命名空间：Sgr.Foundation.API.Application.DataCategory.Validations
 * 创建时间：2023/8/18 19:34:34
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.Foundation.API.Application.DataCategory.Commands;

namespace Sgr.Foundation.API.Application.DataCategory.Validations
{
    public class CreateCategpryItemCommandValidator : AbstractValidator<CreateCategpryItemCommand>
    {
        public CreateCategpryItemCommandValidator()
        {
            RuleFor(command => command.CategoryTypeCode).NotEmpty().MaximumLength(200);
            RuleFor(command => command.DcItemName).NotEmpty().MaximumLength(200);
            RuleFor(command => command.DcItemValue).NotEmpty().MaximumLength(200);
            RuleFor(command => command.Remarks).MaximumLength(200);
            
        }
    }
}
