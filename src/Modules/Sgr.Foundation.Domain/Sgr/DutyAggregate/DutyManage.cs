using Sgr.Domain.Entities;
using Sgr.Generator;
using Sgr.OrganizationAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgr.DutyAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class DutyManage : IDutyManage
    {
        private readonly IDutyRepository _dutyRepository;
        private readonly INumberIdGenerator _numberIdGenerator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dutyRepository"></param>
        /// <param name="numberIdGenerator"></param>
        public DutyManage(IDutyRepository dutyRepository,
            INumberIdGenerator numberIdGenerator)
        {
            _dutyRepository = dutyRepository;
            _numberIdGenerator = numberIdGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="orderNumber"></param>
        /// <param name="remarks"></param>
        /// <param name="state"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public async Task<Duty> CreateNewAsync(string code, string name, int orderNumber, string? remarks, EntityStates state, long orgId = 0)
        {
            //不可为空
            Check.StringNotNullOrWhiteSpace(code, nameof(code));
            Check.StringNotNullOrWhiteSpace(name, nameof(name));

            //检查职务编码是否符合业务要求
            var id = _numberIdGenerator.GenerateUniqueId();
            (await IsUniqueAsync(code, id,orgId)).HandleRule();

            return new Duty(code)
            {
                Id = id,
                OrderNumber = orderNumber,
                Name = name,
                Remarks = remarks,
                State = state,
                OrgId = orgId
            };
        }

        /// <summary>
        /// 检查职务编码是否符合业务要求
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>
        /// <param name="orgId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BusinessCheckResult> IsUniqueAsync(
            string code,
            long id = 0,
            long orgId = 0,
            CancellationToken cancellationToken = default)
        {
            if (await _dutyRepository.ExistAsync(code, id, orgId, cancellationToken))
                return BusinessCheckResult.Fail("职务编码已存在");
            return BusinessCheckResult.Ok();
        }


    }
}
