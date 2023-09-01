using System.ComponentModel.DataAnnotations;

namespace LoginService.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string Roles { get; set; }
    }
}
