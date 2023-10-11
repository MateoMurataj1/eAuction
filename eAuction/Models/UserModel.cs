using System.ComponentModel.DataAnnotations;

namespace eAuction.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Wallet { get; set; }
    }
}
