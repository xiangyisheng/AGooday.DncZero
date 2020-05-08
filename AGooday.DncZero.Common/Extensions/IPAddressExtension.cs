using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AGooday.DncZero.Common.Extensions
{
    public static class IPAddressExtension
    {
		public static string ToIPv4String(this IPAddress address)
		{
			string text = (address ?? IPAddress.IPv6Loopback).ToString();
			if (!text.StartsWith("::ffff:"))
			{
				return text;
			}
			return (address ?? IPAddress.IPv6Loopback).MapToIPv4().ToString();
		}
	}
}
