using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Twilio;
using YandS.UI.Models;

namespace YandS.UI
{
    public class EmailService : IIdentityMessageService
    {
        private const string cKey_SmtpServer = "SmtpServer";
        private const string cKey_SmtpPort = "SmtpPort";
        private const string cKey_SmtpUsername = "SmtpUsername";
        private const string cKey_SmtpPassword = "SmtpPassword";
        private const string cKey_SmtpEMailFrom = "SmtpEMailFrom";
        private const string cKey_SmtpNetworkDeliveryMethodEnabled = "SmtpNetworkDeliveryMethodEnabled";

        private readonly string m_Server;
        private readonly int m_Port;
        private readonly string m_Username;
        private readonly string m_Password;
        private readonly string m_EMailFrom;
        private readonly bool m_IsSmtpNetworkDeliveryMethodEnabled;

        //public Task SendAsync(IdentityMessage message)
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.           
            bool IsSmtpServer = RBAC_ExtendedMethods.GetConfigSettingAsBool("");
            //await Send(message);
            Send2HotmailAccount(message);
        }

        public EmailService()
        {
            this.m_Server = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpServer);
            this.m_Port = RBAC_ExtendedMethods.GetConfigSettingAsInt(cKey_SmtpPort);
            this.m_Username = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpUsername);
            this.m_Password = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpPassword);
            this.m_EMailFrom = RBAC_ExtendedMethods.GetConfigSetting(cKey_SmtpEMailFrom);
            this.m_IsSmtpNetworkDeliveryMethodEnabled = RBAC_ExtendedMethods.GetConfigSettingAsBool(cKey_SmtpNetworkDeliveryMethodEnabled);
        }

        private async Task Send(IdentityMessage message)
        {
            Send2HotmailAccount(message);
            return;

            // Configure Email client.
            SmtpClient client = new SmtpClient(this.m_Server);
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create credentials:
            NetworkCredential credentials = new NetworkCredential(this.m_Username, this.m_Password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            // WARNING: This switches of certificate validation!
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            // Create the message:
            var mail = new MailMessage(new MailAddress(this.m_Username, "Administator"), new MailAddress(message.Destination, message.Destination));
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            // Send:
            if (mail != null)
            {
                await client.SendMailAsync(mail);
            }
            else
            {
                //Trace.TraceError("Failed to send email.");
                await Task.FromResult(0);
            }
        }

        private bool Send2HotmailAccount(IdentityMessage message)
        {
            bool _retVal = false;
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(this.m_EMailFrom);
                    mail.To.Add(message.Destination);
                    mail.Subject = message.Subject;
                    mail.Body = message.Body;
                    //mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Plain));
                    mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html));
                    //mail.ReplyToList.Add(new MailAddress(this.m_Username));

                    var smtp = new SmtpClient(this.m_Server, this.m_Port);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(this.m_Username, this.m_Password);
                    smtp.Timeout = 60000; // 60 seconds
                    smtp.EnableSsl = true; // Outlook.com and Gmail require SSL

                    if (this.m_IsSmtpNetworkDeliveryMethodEnabled)
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    }

                    smtp.Send(mail);

                    // email was accepted by the SMTP server
                    _retVal = true;
                }
            }
            catch (Exception e)
            {
                string mess = e.Message;
                // TODO: Log the exception message
                return false;
            }
            return _retVal;
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            var Twilio = new TwilioRestClient(ConfigurationManager.AppSettings["SMSSid"],
            ConfigurationManager.AppSettings["SMSToken"]);

            var result = Twilio.SendMessage(ConfigurationManager.AppSettings["SMSFromPhone"], message.Destination, message.Body, "");

            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IUserStore<ApplicationUser, int>
    {
        public ApplicationUserStore(RBACDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<RBACDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = RBAC_ExtendedMethods.GetConfigSettingAsInt(RBAC_ExtendedMethods.cKey_PasswordRequiredLength, 6),
                RequireNonLetterOrDigit = RBAC_ExtendedMethods.GetConfigSettingAsBool(RBAC_ExtendedMethods.cKey_PasswordRequireNonLetterOrDigit, true),
                RequireDigit = RBAC_ExtendedMethods.GetConfigSettingAsBool(RBAC_ExtendedMethods.cKey_PasswordRequireDigit, true),
                RequireLowercase = RBAC_ExtendedMethods.GetConfigSettingAsBool(RBAC_ExtendedMethods.cKey_PasswordRequireLowercase, true),
                RequireUppercase = RBAC_ExtendedMethods.GetConfigSettingAsBool(RBAC_ExtendedMethods.cKey_PasswordRequireUppercase, true),
            };

            // Configure user lockout defaults          
            manager.UserLockoutEnabledByDefault = RBAC_ExtendedMethods.GetConfigSettingAsBool(RBAC_ExtendedMethods.cKey_UserLockoutEnabled);
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(RBAC_ExtendedMethods.GetConfigSettingAsDouble(RBAC_ExtendedMethods.cKey_AccountLockoutTimeSpan));
            manager.MaxFailedAccessAttemptsBeforeLockout = RBAC_ExtendedMethods.GetConfigSettingAsInt(RBAC_ExtendedMethods.cKey_MaxFailedAccessAttemptsBeforeLockout);

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser, int>
            {
                MessageFormat = "Your security code is {0}"
            });

            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public static ApplicationUser GetUser(int _userId)
        {
            return GetUser(new RBACDbContext(), _userId);
        }

        public static ApplicationUser GetUser(RBACDbContext db, int _userId)
        {
            ApplicationUser _retVal = null;
            try
            {
                _retVal = db.Users.Where(p => p.Id == _userId).Include("Roles").Include(x => x.Roles.Select(r => r.Role.PERMISSIONS)).FirstOrDefault();
            }
            catch (Exception)
            {
            }

            return _retVal;
        }

        public static List<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.Users.Where(r => r.Inactive == false || r.Inactive == null).OrderBy(r => r.Lastname).ThenBy(r => r.Firstname).ToList();
                }
            }
            catch (Exception)
            {
            }

            return _retVal;
        }

        public static List<ApplicationUser> GetUsers4Surname(string _surname)
        {
            List<ApplicationUser> _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.Users.Where(r => r.Inactive == false || r.Inactive == null & r.Lastname == _surname).OrderBy(r => r.Lastname).ThenBy(r => r.Firstname).ToList();
                }
            }
            catch (Exception)
            {
            }

            return _retVal;
        }

        public static bool AddUser2Role(int _userId, int _roleId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationUser _user = GetUser(db, _userId);
                    if (_user.Roles.Where(p => p.RoleId == _roleId).Count() == 0)
                    {
                        //_user.UserRoles.Add(_role);

                        ApplicationUserRole _identityRole = new ApplicationUserRole { UserId = _userId, RoleId = _roleId };
                        if (!_user.Roles.Contains(_identityRole))
                            _user.Roles.Add(_identityRole);

                        _user.LastModified = DateTime.Now;
                        db.Entry(_user).State = EntityState.Modified;
                        db.SaveChanges();

                        _retVal = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool RemoveUser4Role(int _userId, int _roleId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationUser _user = GetUser(db, _userId);
                    if (_user.Roles.Where(p => p.RoleId == _roleId).Count() > 0)
                    {
                        _user.Roles.Remove(_user.Roles.Where(p => p.RoleId == _roleId).FirstOrDefault());
                        _user.LastModified = DateTime.Now;
                        db.Entry(_user).State = EntityState.Modified;
                        db.SaveChanges();

                        _retVal = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool DeleteUser(int _userId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    //ApplicationUser _user = db.Users.Where(p => p.Id == _userId).Include("ROLES").FirstOrDefault();                 
                    ApplicationUser _user = GetUser(db, _userId);

                    _user.Roles.Clear();
                    db.Entry(_user).State = EntityState.Deleted;
                    db.SaveChanges();

                    _retVal = true;
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool UpdateUser(UserViewModel _user)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationUser _user2Modify = GetUser(db, _user.Id);

                    db.Entry(_user2Modify).Entity.UserName = _user.UserName;
                    db.Entry(_user2Modify).Entity.Email = _user.Email;
                    db.Entry(_user2Modify).Entity.Firstname = _user.Firstname;
                    db.Entry(_user2Modify).Entity.Lastname = _user.Lastname;
                    db.Entry(_user2Modify).Entity.LastModified = System.DateTime.Now;
                    db.Entry(_user2Modify).State = EntityState.Modified;
                    db.SaveChanges();

                    _retVal = true;
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return _retVal;
        }
        public static bool CreateUser(UserViewModel _user)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(db));

                    var user = new ApplicationUser { UserName = _user.UserName, Email = _user.Email, Firstname = _user.Firstname, Lastname = _user.Lastname, LastModified = DateTime.Now, Inactive = false, EmailConfirmed = true };
                    var result = UserManager.Create(user, "Oman123");
                    _retVal = true;
                }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            return _retVal;
        }

        public static List<ApplicationUser> GetUsers4SelectList()
        {
            List<ApplicationUser> _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.Users.Where(r => r.Inactive == false || r.Inactive == null).ToList();
                }
            }
            catch (Exception)
            {
            }

            return _retVal;
        }

    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
    {
        public ApplicationRoleStore(RBACDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> store)
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<ApplicationRole, int, ApplicationUserRole>(context.Get<RBACDbContext>()));
        }

        public static List<ApplicationRole> GetRoles()
        {
            List<ApplicationRole> _retVal = null;
            try
            {
                using (RoleStore<ApplicationRole, int, ApplicationUserRole> db = new RoleStore<ApplicationRole, int, ApplicationUserRole>(new RBACDbContext()))
                {
                    _retVal = db.Roles.Include("PERMISSIONS").ToList();
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static ApplicationRole GetRole(int _roleId)
        {
            ApplicationRole _retVal = null;
            try
            {
                using (RoleStore<ApplicationRole, int, ApplicationUserRole> db = new RoleStore<ApplicationRole, int, ApplicationUserRole>(new RBACDbContext()))
                {
                    _retVal = db.Roles.Where(p => p.Id == _roleId).Include("PERMISSIONS").FirstOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool CreateRole(ApplicationRole _role)
        {
            bool _retVal = false;
            try
            {
                var roleManager = new RoleManager<ApplicationRole, int>(new RoleStore<ApplicationRole, int, ApplicationUserRole>(new RBACDbContext()));
                if (!roleManager.RoleExists(_role.Name))
                {
                    //_role.Id = Guid.NewGuid().ToString();
                    _role.LastModified = DateTime.Now;
                    roleManager.Create(_role);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _retVal;
        }

        public static bool AddPermission2Role(int _roleId, int _permissionId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationRole role = db.Roles.Where(p => p.Id == _roleId).Include("PERMISSIONS").FirstOrDefault();
                    if (role != null)
                    {
                        PERMISSION _permission = db.PERMISSIONS.Where(p => p.PermissionId == _permissionId).Include("ROLES").FirstOrDefault();
                        if (!role.PERMISSIONS.Contains(_permission))
                        {
                            role.PERMISSIONS.Add(_permission);
                            role.LastModified = DateTime.Now;
                            db.Entry(role).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return _retVal;
        }

        public static bool AddAllPermissions2Role(int _roleId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationRole role = db.Roles.Where(p => p.Id == _roleId).Include("PERMISSIONS").FirstOrDefault();
                    if (role != null)
                    {
                        List<PERMISSION> _permissions = db.PERMISSIONS.Include("ROLES").ToList();
                        foreach (PERMISSION _permission in _permissions)
                        {
                            if (!role.PERMISSIONS.Contains(_permission))
                            {
                                role.PERMISSIONS.Add(_permission);
                            }
                        }
                        role.LastModified = DateTime.Now;
                        db.Entry(role).State = EntityState.Modified;
                        db.SaveChanges();
                        _retVal = true;
                    }
                }
            }
            catch
            {
            }
            return _retVal;
        }

        public static bool UpdateRole(RoleViewModel _modifiedRole)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationRole _role2Modify = db.Roles.Where(p => p.Id == _modifiedRole.Id).Include("PERMISSIONS").FirstOrDefault();

                    db.Entry(_role2Modify).Entity.Name = _modifiedRole.Name;
                    db.Entry(_role2Modify).Entity.RoleDescription = _modifiedRole.RoleDescription;
                    db.Entry(_role2Modify).Entity.IsSysAdmin = _modifiedRole.IsSysAdmin;
                    db.Entry(_role2Modify).Entity.LastModified = System.DateTime.Now;
                    db.Entry(_role2Modify).State = EntityState.Modified;
                    db.SaveChanges();

                    _retVal = true;
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool DeleteRole(int _roleId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationRole _role2Delete = db.Roles.Where(p => p.Id == _roleId).Include("PERMISSIONS").FirstOrDefault();
                    if (_role2Delete != null)
                    {
                        _role2Delete.PERMISSIONS.Clear();
                        db.Entry(_role2Delete).State = EntityState.Deleted;
                        db.SaveChanges();
                        _retVal = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool RemovePermission4Role(int _roleId, int _permissionId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    ApplicationRole _role2Modify = db.Roles.Where(p => p.Id == _roleId).Include("PERMISSIONS").FirstOrDefault();
                    PERMISSION _permission = db.PERMISSIONS.Where(p => p.PermissionId == _permissionId).Include("ROLES").FirstOrDefault();

                    if (_role2Modify.PERMISSIONS.Contains(_permission))
                    {
                        _role2Modify.PERMISSIONS.Remove(_permission);
                        _role2Modify.LastModified = DateTime.Now;
                        db.Entry(_role2Modify).State = EntityState.Modified;
                        db.SaveChanges();

                        _retVal = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static List<ApplicationRole> GetRoles4SelectList()
        {
            List<ApplicationRole> _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.Roles.OrderBy(p => p.Name).ToList();
                }
            }
            catch (Exception)
            {
            }

            return _retVal;
        }

        public static List<PERMISSION> GetPermissions4SelectList()
        {
            List<PERMISSION> _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.PERMISSIONS.OrderBy(p => p.PermissionDescription).ToList();
                }
            }
            catch (Exception)
            {
            }

            return _retVal;
        }

        #region Worker functions for Permissions
        public static List<PERMISSION> GetPermissions()
        {
            List<PERMISSION> _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.PERMISSIONS.OrderBy(p => p.PermissionDescription).Include("ROLES").ToList();
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static PERMISSION GetPermission(int _permissionid)
        {
            PERMISSION _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.PERMISSIONS.Where(p => p.PermissionId == _permissionid).Include("ROLES").FirstOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        /*public static PERMISSION GetPermission4Description(string _permDescription)
        {
            PERMISSION _retVal = null;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    _retVal = db.PERMISSIONS.Where(p => p.PermissionDescription == _permDescription).Include("ROLES").FirstOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }*/


        public static bool AddPermission(PERMISSION _newPermission)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    db.PERMISSIONS.Add(_newPermission);
                    db.Entry(_newPermission).State = EntityState.Added;
                    db.SaveChanges();
                    _retVal = true;
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool UpdatePermission(PERMISSION _permission)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    db.Entry(_permission).State = EntityState.Modified;
                    db.SaveChanges();
                    _retVal = true;
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }

        public static bool DeletePermission(int _permissionId)
        {
            bool _retVal = false;
            try
            {
                using (RBACDbContext db = new RBACDbContext())
                {
                    PERMISSION _permission = db.PERMISSIONS.Where(p => p.PermissionId == _permissionId).Include("ROLES").FirstOrDefault();

                    _permission.ROLES.Clear();
                    db.Entry(_permission).State = EntityState.Deleted;
                    db.SaveChanges();
                    _retVal = true;
                }
            }
            catch (Exception)
            {
            }
            return _retVal;
        }
        #endregion
    }

}
