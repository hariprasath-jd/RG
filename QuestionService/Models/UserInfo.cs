using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginService.Models
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Data")]
        public int UserId { get; set; }

        public String? Name { get; set; }

        public string? Address { get; set; }

        public string? FileName { get; set; }

        public byte[]? ProfileImage { get; set; }

        public string? MobileNo  { get; set; }

        public UserData? Data { get; set; }
    }
}
