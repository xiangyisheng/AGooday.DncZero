using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace AGooday.DncZero.Common.Helper
{
    public static class SystemHelper
    {
        /// <summary>
        /// 获得本机物理网卡的MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddressByNetworkInformation()
        {
            //string key = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\";
            string key = @"SYSTEM\CurrentControlSet\Control\Network\{4D36E972-E325-11CE-BFC1-08002BE10318}\";
            string macAddress = string.Empty;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                        && adapter.GetPhysicalAddress().ToString().Length != 0)
                    {
                        //string fRegistryKey = key + adapter.Id + "\\Connection";
                        string fRegistryKey = key + adapter.Id + @"\Connection";
                        RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                        if (rk != null)
                        {
                            string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                            int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                            if (fPnpInstanceID.Length > 3 &&
                                fPnpInstanceID.Substring(0, 3) == "PCI")
                            {
                                macAddress = adapter.GetPhysicalAddress().ToString();
                                for (int i = 1; i < 6; i++)
                                {
                                    macAddress = macAddress.Insert(3 * i - 1, ":");
                                }
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //这里写异常的处理
            }
            return macAddress;
        }

        /// <summary>
        /// 获取本地ipv4地址
        /// </summary>
        /// <param name="IP"></param>
        public static void GetLocalIP(ref string IP)
        {
            string hostname = Dns.GetHostName();
            IPAddress[] ipalist = Dns.GetHostAddresses(hostname);

            string ipv4 = string.Empty;
            string ipv6 = string.Empty;
            foreach (IPAddress ipa in ipalist)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                //以上说明内容参考https://gitee.com/sunovo/codes/s8mv457dxet96fzrbchna62

                if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipv4 = ipa.ToString();
                    break;
                }

                if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    ipv6 = ipa.ToString();
                    break;
                }
            }

            IP = string.IsNullOrWhiteSpace(ipv4) ? ipv6 : ipv4;
        }
    }
}
