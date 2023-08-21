/**************************************************************
 * 
 * 唯一标识：23d7abe5-f26d-4e5d-862c-5581824b195c
 * 命名空间：Sgr.Indentity.Controllers
 * 创建时间：2023/8/21 11:34:49
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sgr.Application.Services;
using Sgr.AspNetCore.ActionFilters;
using Sgr.Domain.Entities;
using Sgr.Exceptions;
using Sgr.Identity;
using Sgr.Identity.Services;
using Sgr.Indentity.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sgr.Indentity.Controllers
{
    /// <summary>
    /// 认证服务
    /// </summary>
    //[Authorize]
    [Route("api/sgr/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class OAuthController : ControllerBase
    {
        private readonly ISignatureChecker _signatureChecker;
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        private readonly JwtOptions _jwtOptions;


        public OAuthController(ISignatureChecker signatureChecker, 
            IAccountService accountService,
            IJwtService jwtService,
            JwtOptions jwtOptions)
        {
            _signatureChecker = signatureChecker;
            _accountService = accountService;
            _jwtService = jwtService;
            _jwtOptions = jwtOptions;
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("token")]
        public async Task<ActionResult<TokenModel>> TokenAsync([FromBody] UserLoginModel model)
        {
            Check.NotNull(model, nameof(model));

            Check.StringNotNullOrEmpty(model.Name, nameof(model.Name));
            Check.StringNotNullOrEmpty(model.Password, nameof(model.Password));

            if(!_signatureChecker.VerifySignature(model.Signature, model.Timestamp, model.Nonce, $"{model.Name}-{model.Password}"))
                throw new BusinessException("获取令牌时，参数中的签名Signature验证失败!");

            var loginResult = await _accountService.ValidateAccountAsync(model.Name, model.Password);

            switch (loginResult.Item1)
            {
                case AccountLoginResults.Success:

                    if (loginResult.Item2 == null)
                        throw new BusinessException("创建令牌时的账号信息为空!");

                    TokenModel tokenModel = await creatTokenModel(loginResult.Item2!);
                    return Ok(tokenModel);
                case AccountLoginResults.IsDeactivate:
                    throw new BusinessException("账号被禁用!");
                case AccountLoginResults.WrongPassword:
                case AccountLoginResults.NotExist:
                default:
                    throw new BusinessException("账号或密码错误!");
            }

        }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<TokenModel>> RefreshTokenAsync([FromBody] RefreshTokenModel model)
        {
            Check.NotNull(model, nameof(model));

            Check.StringNotNullOrEmpty(model.RefrashToken, nameof(model.RefrashToken));
            Check.StringNotNullOrEmpty(model.AccessToken, nameof(model.AccessToken));

            ClaimsPrincipal? claimsPrincipal = _jwtService.ValidateAccessToken(model.AccessToken!, _jwtOptions);
            if(claimsPrincipal == null)
                throw new BusinessException("AccessToken无法解析!");

            var loginName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if(loginName == null)
                throw new BusinessException("AccessToken中无法获取用户标识!");

            var validateResult = await _accountService.ValidateRefreshTokenAsync(loginName, model.RefrashToken!);
            switch(validateResult.Item1) 
            {
                case ValidateRefreshTokenResults.Success:

                    if (validateResult.Item2 == null)
                        throw new BusinessException("刷新令牌时的账号信息为空!");

                    TokenModel tokenModel = await creatTokenModel(validateResult.Item2!);
                    return Ok(tokenModel);
                case ValidateRefreshTokenResults.Expire:
                    throw new BusinessException("RefreshToken已过期!");
                case ValidateRefreshTokenResults.NotExist:
                default:
                    throw new BusinessException("RefreshToken不存在!");
            }
        }


        private async Task<TokenModel> creatTokenModel(Account account)
        {
            TokenModel tokenModel = new TokenModel();
            //创建访问令牌
            var claims = new Claim[]
            {
                        new Claim(ClaimTypes.Name, account.LoginName),
                        new Claim(JwtRegisteredClaimNames.Sub, account.Id),
                        new Claim(Constant.CLAIM_USER_ORGID, account.OrgId),
            };
            tokenModel.AccessToken = _jwtService.CreateAccessToken(claims, _jwtOptions);
            //创建刷新令牌
            tokenModel.RefrashToken = _jwtService.CreateRefreshToken();
            await _accountService.CreateRefreshTokenAsync(account.LoginName, tokenModel.RefrashToken);

            return tokenModel;
        }


    }
}
