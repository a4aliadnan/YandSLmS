using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YandS.UI.Models
{
    public class EMailMessage
    {
        private const string cKey_SmtpServer = "SmtpServer";
        private const string cKey_SmtpPort = "SmtpPort";
        private const string cKey_SmtpUsername = "SmtpUsername";
        private const string cKey_SmtpPassword = "SmtpPassword";
        private const string cKey_SmtpEMailFrom = "SmtpEMailFrom";
        private const string cKey_SmtpNetworkDeliveryMethodEnabled = "SmtpNetworkDeliveryMethodEnabled";

        public readonly string m_Server;
        public readonly int m_Port;
        public readonly string m_Username;
        public readonly string m_Password;
        public readonly string m_EMailFrom;
        public readonly bool m_IsSmtpNetworkDeliveryMethodEnabled;

        public string Destination { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EMailMessage()
        {
            this.m_Server = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpServer);
            this.m_Port = RBAC_ExtendedMethods.GetConfigSettingAsInt(cKey_SmtpPort);
            this.m_Username = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpUsername);
            this.m_Password = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpPassword);
            this.m_EMailFrom = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpEMailFrom);
            this.m_IsSmtpNetworkDeliveryMethodEnabled = RBAC_ExtendedMethods.GetConfigSettingAsBool(cKey_SmtpNetworkDeliveryMethodEnabled);
        }
    }

}