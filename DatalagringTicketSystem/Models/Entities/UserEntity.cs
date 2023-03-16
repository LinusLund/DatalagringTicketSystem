using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;



namespace DatalagringTicketSystem.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    internal class UserEntity
    {
        [Key]
        public Guid UserId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string Email { get; set; } = null!;

        [StringLength(14)]
        public string PhoneNumber { get; set; } = null!;

        public ICollection<TicketEntity> Users = new HashSet<TicketEntity>();


        public static implicit operator UserModel(UserEntity userEntity)
        {
            return new UserModel
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                PhoneNumber = userEntity.PhoneNumber
            };

        }

        public static implicit operator UserEntity(UserModel userEntity)
        {
            return new UserEntity
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                PhoneNumber = userEntity.PhoneNumber
            };

        }
    }

 
}


