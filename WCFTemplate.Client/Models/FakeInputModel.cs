using System.ComponentModel.DataAnnotations;

namespace WCFTemplate.Client.Models
{
    public class FakeInputModel
    {
        private const string MsgTitle = "Wrong title.";
        private const string MsgLastname = "Wrong last name.";
        private const string MsgFirstname = "Wrong first name.";
        private const string MsgEmail = "Wrong email.";
        
        [Required(AllowEmptyStrings = false, ErrorMessage = MsgTitle)]
        [MaxLength(5, ErrorMessage = MsgTitle), MinLength(1, ErrorMessage = MsgTitle)]
        public string ClientTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = MsgLastname)]
        [MaxLength(255, ErrorMessage = MsgLastname), MinLength(1, ErrorMessage = MsgLastname)]
        public string ClientLastname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = MsgFirstname)]
        [MaxLength(255, ErrorMessage = MsgFirstname), MinLength(1, ErrorMessage = MsgFirstname)]
        public string ClientFirstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = MsgEmail)]
        [MaxLength(255, ErrorMessage = MsgEmail), MinLength(5, ErrorMessage = MsgEmail)]
        public string ClientEmail { get; set; }
    }
}