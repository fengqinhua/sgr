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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Sgr.Application;
using Sgr.AspNetCore.ActionFilters;
using Sgr.AuditLogs;
using Sgr.Domain.Entities;
using Sgr.Domain.Repositories;
using Sgr.ExceptionHandling;
using Sgr.Exceptions;
using Sgr.Identity;
using Sgr.Identity.Services;
using Sgr.Indentity.Utilities;
using Sgr.Indentity.ViewModels;
using Sgr.Utilities;
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
    [Route("api/sgr/[controller]")]
    [ApiController]
    //[AllowAnonymous]
    //[Authorize]
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


        [HttpGet]
        [Route("captcha")]
        public FileResult GetCaptcha()
        {
            Tuple<string, byte[]> captchaCode = _jwtOptions.CaptchaIsArithmetic ? CaptchaHelper.CreateArithmeticCaptcha() : CaptchaHelper.CreateCaptcha();
            this.HttpContext.Response.Headers["sgr-captcha"] = buildCaptchaCode(captchaCode.Item1);
            return File(captchaCode.Item2, @"image/jpeg");
        }


        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenModel>> TokenAsync([FromBody] UserLoginModel model)
        {
            Check.NotNull(model, nameof(model));

            Check.StringNotNullOrEmpty(model.Name, nameof(model.Name));
            Check.StringNotNullOrEmpty(model.Password, nameof(model.Password));

            if (_jwtOptions.UseSignature)
            {
                if (!_signatureChecker.VerifySignature(model.Signature, model.Timestamp, model.Nonce, $"{model.Name}-{model.Password}"))
                    return this.CustomBadRequest("获取令牌时，参数中的签名Signature验证失败!");
            }

            if (_jwtOptions.UseCaptcha)
            {
                if (model.VerificationHash != buildCaptchaCode(model.VerificationCode))
                    return this.CustomBadRequest("验证码错误!");
            }

            var loginResult = await _accountService.ValidateAccountAsync(model.Name, model.Password);

            if (loginResult.Item1 == AccountLoginResults.Success)
            {
                if (loginResult.Item2 != null)
                {
                    TokenModel tokenModel = await creatTokenModel(loginResult.Item2!);
                    await createLoginLog(loginResult.Item2!.OrgId, loginResult.Item2!.LoginName, false, true, "");
                    return Ok(tokenModel);
                }
                else
                {
                    string msg = "创建令牌时的账号信息为空!";
                    await createLoginLog("", model.Name, false, false, msg);
                    return this.CustomBadRequest(msg);
                }
            }
            else
            {
                string msg = getMessageFromAccountLoginResults(loginResult.Item1);
                await createLoginLog(loginResult.Item2?.OrgId ?? "", model.Name, false, false, msg);
                return this.CustomBadRequest(msg);
            }
        }


        /// <summary>
        /// 刷新令牌
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TokenModel>> RefreshTokenAsync([FromBody] RefreshTokenModel model)
        {
            Check.NotNull(model, nameof(model));
            Check.StringNotNullOrEmpty(model.RefrashToken, nameof(model.RefrashToken));
            Check.StringNotNullOrEmpty(model.AccessToken, nameof(model.AccessToken));

            if (_jwtOptions.UseSignature)
            {
                if (!_signatureChecker.VerifySignature(model.Signature, model.Timestamp, model.Nonce, $"{model.AccessToken}-{model.RefrashToken}"))
                    return this.CustomBadRequest("刷新令牌时，参数中的签名Signature验证失败!");
            }

            ClaimsPrincipal? claimsPrincipal = _jwtService.ValidateAccessToken(model.AccessToken!, _jwtOptions);
            if(claimsPrincipal == null)
                return this.CustomBadRequest("AccessToken无法解析!");

            var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if(userId == null)
                return this.CustomBadRequest("AccessToken中无法获取用户标识!");

            var validateResult = await _accountService.ValidateRefreshTokenAsync(userId, model.RefrashToken!);

            if(validateResult.Item1 == ValidateRefreshTokenResults.Success)
            {
                if(validateResult.Item2 != null)
                {
                    TokenModel tokenModel = await creatTokenModel(validateResult.Item2!);
                    await createLoginLog(validateResult.Item2!.OrgId, validateResult.Item2!.LoginName, true, true, "");
                    return Ok(tokenModel);

                }
                else
                {
                    string msg = "刷新令牌时的账号信息为空!";

                    var userName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var orgId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == Constant.CLAIM_USER_ORGID)?.Value;
                    await createLoginLog(orgId ?? "", userName ?? "", true, false, msg);

                    return this.CustomBadRequest(msg);
                }
            }
            else
            {
                string msg = getMessageFromValidateRefreshTokenResults(validateResult.Item1);
                var userName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                await createLoginLog(validateResult.Item2?.OrgId ?? "", userName ?? "", true, false, msg);
                return this.CustomBadRequest(msg);
            }
        }

        private async Task<TokenModel> creatTokenModel(Account account)
        {
            TokenModel tokenModel = new TokenModel();
            //创建访问令牌
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, account.LoginName),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(Constant.CLAIM_USER_ORGID, account.OrgId.ToString()),
            };
            tokenModel.AccessToken = _jwtService.CreateAccessToken(claims, _jwtOptions);
            //创建刷新令牌
            tokenModel.RefrashToken = _jwtService.CreateRefreshToken();
            await _accountService.CreateRefreshTokenAsync(account.LoginName, tokenModel.RefrashToken);

            return tokenModel;
        }

        private Task createLoginLog(string orgId,string loginName, bool fromRefreshToken, bool status, string remarks)
        {
            var httpUserAgentProvider = this.HttpContext.RequestServices.GetRequiredService<IHttpUserAgentProvider>();
            var httpUserAgent = httpUserAgentProvider.Analysis(this.HttpContext);

            return _accountService.CreateLoginLog(loginName,
               this.HttpContext.GetClientIpAddress(),
               fromRefreshToken ? "账号密码登录" : "更新令牌",
               httpUserAgent.BrowserInfo,
               httpUserAgent.Os,
               status,
               remarks,
               orgId);
        }

        private static string getMessageFromAccountLoginResults(AccountLoginResults accountLoginResults)
        {
            return accountLoginResults switch
            {
                AccountLoginResults.IsDeactivate => "账号被禁用!",
                AccountLoginResults.IsDelete => "账号被删除!",
                AccountLoginResults.IsLock => "账号被锁定!",
                AccountLoginResults.WrongPassword or AccountLoginResults.NotExist => "账号或密码错误!",
                AccountLoginResults.Success => "成功",
                _ => "未知的",
            };
        }

        private static string getMessageFromValidateRefreshTokenResults(ValidateRefreshTokenResults validateRefreshTokenResults)
        {
            return validateRefreshTokenResults switch
            {
                ValidateRefreshTokenResults.Expire => "RefreshToken已过期!",
                ValidateRefreshTokenResults.AccountAbnormal => "账号异常!",
                ValidateRefreshTokenResults.NotExist => "RefreshToken不存在!",
                ValidateRefreshTokenResults.Success => "成功",
                _ => "未知的",
            };
        }

        private static string buildCaptchaCode(string code)
        {
            return HashHelper.CreateMd5($"sgr-{code.ToLower()}-captcha");
        }

    }
}
