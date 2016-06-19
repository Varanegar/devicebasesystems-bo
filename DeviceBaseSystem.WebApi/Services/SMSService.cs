using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DeviceBaseSystem.WebApi.Services
{
    public class SMSService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configAtiyeSmsAsync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task configAtiyeSmsAsync(IdentityMessage message)
        {


        }
    }
}