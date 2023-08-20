/**************************************************************
 * 
 * 唯一标识：8211c41b-adcd-4884-8450-7f64f11bcb05
 * 命名空间：Sgr.AspNetCore.Middlewares
 * 创建时间：2023/8/18 21:46:22
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using FluentValidation;
using Sgr.AspNetCore.Middlewares;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Sgr.ExceptionHandling
{
    public class DefaultExceptionToErrorInfo : IExceptionToErrorInfo
    {
        private readonly IExceptionHandlingOptions _options;

        public DefaultExceptionToErrorInfo(IExceptionHandlingOptions options)
        {
            _options = options;
        }

        public Tuple<HttpStatusCode, ServiceErrorInfo> Convert(Exception exception)
        {
            HttpStatusCode httpStatusCode = JudgeHttpStatusCode(exception);

            ServiceErrorInfo serviceErrorInfo = new ServiceErrorInfo()
            {
                Message = exception.Message,
                Data = exception.Data,
                ValidationErrors = DisassemblyValidationErrorInfo(exception)
            };

            if (_options.IncludeFullDetails)
            {
                var detailBuilder = new StringBuilder();
                detailBuilder.AppendLine($"ExceptionType : {exception.GetType().FullName} ");
                detailBuilder.AppendLine($"StackTrace : {exception.StackTrace}");
                serviceErrorInfo.Details = detailBuilder.ToString();
            }

            return new Tuple<HttpStatusCode, ServiceErrorInfo>(httpStatusCode, serviceErrorInfo);
        }

        protected virtual HttpStatusCode JudgeHttpStatusCode(Exception exception)
        {
            return HttpStatusCode.InternalServerError;
        }

        protected virtual ServiceValidationErrorInfo[]? DisassemblyValidationErrorInfo(Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                if (validationException.Errors != null)
                {
                    List<ServiceValidationErrorInfo> errors = new List<ServiceValidationErrorInfo>();

                    foreach (var error in validationException.Errors)
                    {
                        errors.Add(new ServiceValidationErrorInfo()
                        {
                            ErrorMessage = error.ErrorMessage,
                            PropertyName = error.PropertyName
                        });
                    }

                    return errors.ToArray();
                }
            }

            return null;
        }
    }
}
