namespace YandS.UI.Models
{
    using System.ComponentModel.DataAnnotations;
    public class VerifyOTPPhoneViewModel
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

    }


    #region TOTP4Email
    public class TOTP4EmailViewModel
    {
        public int UserId { get; set; }
        [Required]
        public string Provider { get; set; }
    }

    public class TOTP4EmailViewModelGet : TOTP4EmailViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class TOTP4EmailViewModelPost : TOTP4EmailViewModel
    {
        [Required]
        [Display(Name = "Security PIN")]
        public string SecurityPIN { get; set; }
    }
    #endregion
}