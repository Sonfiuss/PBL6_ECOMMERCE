using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website_Ecommerce.API.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
<<<<<<< HEAD
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlock { get; set; }
=======
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? UrlAvatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public bool Gender { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsBlock { get; set; }

        public Shipper Shipper { get; set; }
        public IList<UserRole> UserRoles { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Shop Shop { get; set; }
>>>>>>> 31f0e805f63357d227287102869eabfb0c22e234
    }
}