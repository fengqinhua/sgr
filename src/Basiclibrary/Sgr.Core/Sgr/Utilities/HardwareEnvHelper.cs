/**************************************************************
 * 
 * 唯一标识：12d9ebc0-98d4-4266-bc6b-b6aebce7b2b4
 * 命名空间：Sgr.Utilities
 * 创建时间：2023/7/24 9:39:47
 * 机器名称：DESKTOP-S0D075D
 * 创建者：antho
 * 电子邮箱：fengqinhua2016@163.com
 * 描述：
 * 
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Sgr.Utilities
{
    /// <summary>
    /// 硬件环境工具类
    /// </summary>
    public static class HardwareEnvHelper
    {
        /// <summary>
        /// 获取当前计算机设备的IP地址
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetHostServerIps()
        {
            var hostIps = NetworkInterface
                                .GetAllNetworkInterfaces()
                                .Where(network => network.OperationalStatus == OperationalStatus.Up)
                                .Select(network => network.GetIPProperties())
                                .OrderByDescending(properties => properties.GatewayAddresses.Count)
                                .SelectMany(properties => properties.UnicastAddresses)
                                .Where(address => !IPAddress.IsLoopback(address.Address) && address.Address.AddressFamily == AddressFamily.InterNetwork)
                                .ToArray();

            IList<string> ips = new List<string>();
            foreach (var item in hostIps)
            {
                ips.Add(item.Address.ToString());
            }

            return ips;

            //var ip = HttpContext.Connection.LocalIpAddress.MapToIPv4()?.ToString();
        }

        /// <summary>
        /// 获取当前计算机设备的Mac地址
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static IList<string> GetHostServerMacs(string separator = "-")
        {
            IList<string> macs = new List<string>();

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics.Where(c =>
                c.NetworkInterfaceType != NetworkInterfaceType.Loopback && c.OperationalStatus == OperationalStatus.Up))
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                var unicastAddresses = properties.UnicastAddresses;
                if (unicastAddresses.Any(temp => temp.Address.AddressFamily == AddressFamily.InterNetwork))
                {
                    var address = adapter.GetPhysicalAddress();
                    byte[] bytes = address.GetAddressBytes();
                    string mac = "";
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        mac += bytes[i].ToString("X2");

                        if (i != bytes.Length - 1)
                        {
                            mac += separator;
                        }
                    }

                    macs.Add(mac);
                }
            }

            return macs;
        }

        /// <summary>
        /// 获取当前计算机名称
        /// </summary>
        /// <returns></returns>
        public static string GetMachineName()
        {            
            //string OSVersion = Environment.OSVersion.VersionString;
            //string MachineName = Environment.MachineName;
            return Environment.MachineName;
        }
    }
}
