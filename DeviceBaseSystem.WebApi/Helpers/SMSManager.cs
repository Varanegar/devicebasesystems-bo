using Anatoli.DataAccess;
using Anatoli.DataAccess.Models.Identity;
using DeviceBaseSystem.DataAccess;
using DeviceBaseSystem.DataAccess.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Anatoli.Business.Helpers
{

    public class SMSManager
    {
        protected static readonly Logger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        public async Task SendSMS(string phoneNo, string messageBody)
        {
            string uriStr = "";
            try
            {

                using (var client = new HttpClient())
                {
                    messageBody = messageBody.Replace(" ", "%20");
                    uriStr = "http://ws.adpdigital.com/url/send?unicode=1&srcaddress=98200087134&username=varanegar&password=varanegar&dstaddress=" + phoneNo + "&body=" + messageBody + "&clientid=1";

                    var uri = new Uri(uriStr);

                    var response = await client.GetAsync(uri);

                    string textResult = await response.Content.ReadAsStringAsync();
                }
            }
            catch(Exception ex)
            {
                log.Error(uriStr, ex);
            }
        }

        public async Task SendSMS(string phoneNo, SMSBody sms, string value)
        {

            var message = sms.Value.Replace("%1", value);
            phoneNo = phoneNo.Substring(phoneNo.Length - 10);
            phoneNo = "98" + phoneNo;
            await SendSMS(phoneNo, message);
        }

        public async Task SendResetPasswordSMS(User user, AnatoliDbContext dbContext, string pass, SMSBody sms)
        {
            await SendResetPasswordSMS(user, dbContext, pass, sms, "");
        }

        public async Task SendResetPasswordSMS(User user, AnatoliDbContext dbContext, string pass, SMSBody sms, string value)
        {
            string message = "";
            string phoneNumber = "";
            phoneNumber = user.PhoneNumber;
            if (phoneNumber.Length >= 10)
            {
                phoneNumber = phoneNumber.Substring(phoneNumber.Length - 10);
                phoneNumber = "98" + phoneNumber;

                Random rnd = new Random();
                int rndValue = rnd.Next(111111, 999999);
                message = sms.Value.Replace("%1", value) + rndValue;

                var userStore = new AnatoliUserStore(dbContext);
                await userStore.SetResetSMSCodeAsync(user, rndValue.ToString(), pass);
                await SendSMS(phoneNumber, message);
            }
        }

        public class SMSBody
        {
            private SMSBody(string value) { Value = value; }

            public string Value { get; set; }

            public static SMSBody NEW_USER { get { return new SMSBody("با سپاس از اعتماد شما به مجموعه ایگ، کد رهگیری عضویت در باشگاه مشتریان ایگ: "); } }
            public static SMSBody NEW_USER_BACKOFFICE { get { return new SMSBody("عضو محترم باشگاه مشتریان ایگ؛ ضمن خوشامدگویی، لطفاً پس از اولین ورود به وب سایت و  اپلیکیشن ایگ؛ کلمه عبور خود را تغییر دهید."); } }
            public static SMSBody NEW_USER_FORGET_PASSWORD { get { return new SMSBody("کد رهگیری جهت بازیابی کلمه عبور شما در باشگاه مشتریان ایگ: "); } }
            public static SMSBody PURCHASE_CONFIRM { get { return new SMSBody("با سپاس از خرید شما. سفارش شما با شماره %1 با موفقیت ثبت شد.%0A باشگاه مشتریان ایگ"); } }
            public static SMSBody PURCHASE_DELIVERT { get { return new SMSBody("مشتری گرامی سفارش شما با شماره %1 پردازش و جهت ارسال به واحد مرسولات تحویل شد."); } }
        }
    }
}