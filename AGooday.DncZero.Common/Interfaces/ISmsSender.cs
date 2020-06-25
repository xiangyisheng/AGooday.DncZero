using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGooday.DncZero.Common.Interfaces
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
