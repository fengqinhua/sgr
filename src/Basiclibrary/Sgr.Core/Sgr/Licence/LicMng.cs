/**************************************************************
 * 
 * 唯一标识：f652651e-4a4f-44e2-81eb-1cadd42ab2ec
 * 命名空间：Sgr.Licence
 * 创建时间：2023/7/24 11:54:57
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using Sgr.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sgr.Licence
{
    /// <summary>
    /// 许可证管理
    /// </summary>
    public class LicMng
    {
        #region 单例模式

        /// <summary>
        /// 实例
        /// </summary>
        public static LicMng Instance { get { return Nested.instance; } }

        private LicMng() { }
        /// <summary>
        /// 通过嵌套类实现单利模式
        /// </summary>
        class Nested
        {
            static Nested()
            {
            }
            internal static readonly LicMng instance = new LicMng();
        }

        #endregion

        private string registrationCode = "";
        /// <summary>
        /// 授权文件检测结果
        /// </summary>
        private bool _isRegistered = false;
        /// <summary>
        /// 最近一次检测授权文件
        /// </summary>
        private DateTime _lastCheckReistered = DateTime.MinValue;
        /// <summary>
        /// 上一次访问时间
        /// </summary>
        private DateTime _productLastVisitDate = DateTime.MinValue;
        /// <summary>
        /// 授权开始时间
        /// </summary>
        private DateTime _startTime = DateTime.MaxValue;
        /// <summary>
        /// 授权截止时间
        /// </summary>
        private DateTime _endTime = DateTime.MinValue;

        /// <summary>
        /// 获取注册码（取决于计算机名称和网卡地址）
        /// </summary>
        /// <returns></returns>
        public string GetRegistrationCode()
        {
            if (string.IsNullOrEmpty(registrationCode))
            {
                StringBuilder hard = new StringBuilder();
                try
                {
                    hard.Append(HardwareEnvHelper.GetMachineName());

                    IList<string> macs = HardwareEnvHelper.GetHostServerMacs();
                    foreach (var item in macs)
                    {
                        hard.Append("#");
                        hard.Append(item);
                    }

                    string tmp = HashHelper.CreateMd5(hard.ToString());
                    if (tmp.Length < 25)
                    {
                        for (int i = tmp.Length; i <= 25; i++)
                        {
                            tmp = tmp + "A";
                        }
                    }

                    StringBuilder sBuilder = new StringBuilder();
                    sBuilder.Append(tmp.Substring(0, 5));
                    sBuilder.Append("-");
                    sBuilder.Append(tmp.Substring(5, 5));
                    sBuilder.Append("-");
                    sBuilder.Append(tmp.Substring(10, 5));
                    sBuilder.Append("-");
                    sBuilder.Append(tmp.Substring(15, 5));
                    sBuilder.Append("-");
                    sBuilder.Append(tmp.Substring(20, 5));

                    registrationCode = sBuilder.ToString();
                }
                catch
                {

                }
            }

            return registrationCode;


        }

        /// <summary>
        /// 鉴权
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool Authentication(ref string msg)
        {
            bool result = false;

            if (!checkIsRegistered())
            {
                msg = "应用程序尚未激活!";
                return false;
            }

            //更新上一次访问时间
            refrashProductLastVisitDate();

            //当前时间
            DateTime thisTime = DateTime.Now;// (DateTime.Now > _productLastVisitDate) ? DateTime.Now : _productLastVisitDate;

            if (_startTime <= thisTime && thisTime <= _endTime)
            {
                int days = (_endTime - thisTime).Days;
                if (days == 0)
                    msg = "授权已到期，请及时续费以免影响使用！";
                else if (days < 15)
                    msg = $"授权仅剩{days}天，请及时续费以免影响使用！";
                result = true;
            }
            else
                msg = $"授权期限为{_startTime.ToShortDateString()}至{_endTime.ToShortDateString()}，现已到期，请及时续费！";

            if (result)
            {
                //检查是否存在回拨时钟的情况
                if (thisTime < _productLastVisitDate)
                {
                    if (_startTime <= _productLastVisitDate && _productLastVisitDate <= _endTime)
                    {

                    }
                    else
                    {
                        //如果当前时间晚于上一次访问时间，并且上一次访问时间已超出授权时间访问，则认定存在时钟回拨的情况
                        result = false;
                        msg = $"授权已失效，检测发现系统时钟被回拨";
                    }
                }
            }


            return result;
        }

        /// <summary>
        /// 执行注册
        /// </summary>
        /// <param name="zcm">注册码</param>
        /// <param name="sqm">激活码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public bool Registered(string zcm, string sqm, ref string msg)
        {
            bool result = false;
            try
            {
                ActivationInfo activationInfo = parsingActivationCode(sqm);
                if (activationInfo == null)
                {
                    msg = "注册码格式错误!";
                    return result;
                }

                if (zcm == activationInfo.RegistrationCode
                    && activationInfo.StartTime != Constant.MaxDateTime
                    && activationInfo.EndTime != Constant.MinDateTime)
                {
                    //保存授权文件
                    saveLicFile(sqm);
                    //刷新最近一次访问时间
                    refrashProductLastVisitDate();
                    //将应用是否已完成注册的状态重置，以便下次鉴权时重新读取注册信息
                    _isRegistered = false;

                    result = true;
                }
                else
                {
                    msg = "激活码错误或已到期!";
                }
            }
            catch //(Exception ex)
            {
                msg = "激活码错误或已到期";
            }

            return result;
        }

        /// <summary>
        /// 刷新最近一次访问时间
        /// </summary>
        private void refrashProductLastVisitDate()
        {
            DateTime now = DateTime.Now;

            double sinceLastRefresh = (now - _productLastVisitDate).TotalHours;
            if (sinceLastRefresh > 1)
            {
                //距离上一次刷新最近一次访问时间超过一小时后,才重新保存状态
                string lmtmp = System.IO.Path.Combine(LocalFileHelper.GetApplicationDirectory(), "lm.tmp");

                try
                {
                    string strLVT_SPAN = "";
                    if (System.IO.File.Exists(lmtmp))
                    {
                        using (StreamReader reader = new StreamReader(lmtmp, System.Text.Encoding.UTF8))
                        {
                            strLVT_SPAN = reader.ReadToEnd().Trim();
                            reader.Close();
                        }
                    }

                    int LVT_SPAN = 0;
                    if (!int.TryParse(strLVT_SPAN, out LVT_SPAN))
                        LVT_SPAN = 0;

                    //获取基准时间
                    DateTime benchmarkDate = getBenchmarkDate();
                    _productLastVisitDate = benchmarkDate.AddSeconds(LVT_SPAN);
                    if (now > _productLastVisitDate)
                    {
                        _productLastVisitDate = now;

                        int lvt = (int)(now - benchmarkDate).TotalSeconds;
                        if (lvt < 0) lvt = 0;

                        using (FileStream fs1 = new FileStream(lmtmp, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            using (StreamWriter sw = new StreamWriter(fs1))
                            {
                                sw.Write(lvt.ToString());//开始写入值
                                sw.Close();
                                fs1.Close();
                            }
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 判断程序是否已注册
        /// </summary>
        /// <returns></returns>
        private bool checkIsRegistered()
        {
            //500次，第500次调用以后再次重新判断
            if (_isRegistered)
            {
                double hour = (DateTime.Now - _lastCheckReistered).TotalHours;
                //如果此处程序是否已完成注册的状态缓存一个小时，超过一个小时以后再次重新判断
                if (hour > 1)
                    _isRegistered = false;
            }


            if (!_isRegistered)
            {
                try
                {
                    string sqm = "";
                    string path = System.IO.Path.Combine(LocalFileHelper.GetApplicationDirectory(), "sgr-app .lic");
                    if (File.Exists(path))
                    {
                        using (StreamReader reader = new StreamReader(path, System.Text.Encoding.UTF8))
                        {
                            sqm = reader.ReadToEnd().Trim();
                            reader.Close();
                        }

                        ActivationInfo activationInfo = parsingActivationCode(sqm);
                        if (activationInfo != null)
                        {
                            string zcm = GetRegistrationCode();
                            if (zcm == activationInfo.RegistrationCode
                                && activationInfo.StartTime != Constant.MaxDateTime
                                && activationInfo.EndTime != Constant.MinDateTime)
                            {
                                _startTime = activationInfo.StartTime;
                                _endTime = activationInfo.EndTime;

                                _lastCheckReistered = DateTime.Now;
                                _isRegistered = true;
                            }
                        }
                    }
                }
                catch { }
            }



            return _isRegistered;

        }
        /// <summary>
        /// 解析激活码
        /// </summary>
        /// <param name="sqm"></param>
        /// <returns></returns>
        private ActivationInfo parsingActivationCode(string sqm)
        {
            string temp = EncryptionHelper.Decrypt(sqm, false);
            string[] txts = temp.Split('^');
            if (txts.Length >= 5)
            {
                ActivationInfo activationInfo = new ActivationInfo();
                activationInfo.RegistrationCode = txts[0];

                if (long.TryParse(txts[1], out long longCount))
                {
                    if (longCount > 0)
                        activationInfo.NumOfLic = longCount;
                }


                if (long.TryParse(txts[3], out long longStart))
                {
                    if (longStart > 0)
                        activationInfo.StartTime = convertLongDateTime(longStart);
                }

                if (long.TryParse(txts[4], out long longEnd))
                {
                    if (longEnd > 0)
                        activationInfo.EndTime = convertLongDateTime(longEnd);
                }

                return activationInfo;
            }


            return null;
        }

        /// <summary>
        /// 保存激活码
        /// </summary>
        /// <param name="sqm"></param>
        private void saveLicFile(string sqm)
        {

            string path = System.IO.Path.Combine(LocalFileHelper.GetApplicationDirectory(), "mp_ocl.lic");

            if (!File.Exists(path))
                File.Delete(path);

            //创建Lic文件
            using (FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs1))
                {
                    sw.Write(sqm);//开始写入值
                    sw.Close();
                    fs1.Close();
                }
            }
        }

        private DateTime convertLongDateTime(long d)
        {
            DateTime dtStart = getBenchmarkDate();
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }

        private long ConvertDataTimeLong(DateTime dt)
        {
            DateTime dtStart = getBenchmarkDate();
            TimeSpan toNow = dt.Subtract(dtStart);
            long timeStamp = toNow.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }

        /// <summary>
        /// 获取注册与鉴权所用的基准时间
        /// </summary>
        /// <returns></returns>
        private DateTime getBenchmarkDate()
        {
            return DateTime.Parse("2000-1-1");
        }



    }

}
