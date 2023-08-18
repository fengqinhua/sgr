/**************************************************************
 * 
 * 唯一标识：b4601b77-b0c3-40ab-84c7-f952702e0fd1
 * 命名空间：Sgr.Foundation.API.Application.DataCategory.Commands
 * 创建时间：2023/8/17 21:15:39
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using MediatR;
using System.Runtime.Serialization;

namespace Sgr.Foundation.API.Application.DataCategory.Commands
{
    public class DeleteCategpryItemCommand : IRequest<bool>
    {

        public long Id { get; set; }
        public DeleteCategpryItemCommand()
        {

        }

        public DeleteCategpryItemCommand(long id) : this()
        {
            Id = id;
        }
    }
}