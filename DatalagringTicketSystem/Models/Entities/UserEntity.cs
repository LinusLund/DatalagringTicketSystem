using DatalagringTicketSystem.Models.Entities;
using DatalagringTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;



namespace DatalagringTicketSystem.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    internal class UserEntity
    {
        [Key]
        public Guid Id { get; set; }

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
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email
            };

        }

        public static implicit operator UserEntity(UserModel customerEntity)
        {
            return new UserEntity
            {
                Id = customerEntity.Id,
                FirstName = customerEntity.FirstName,
                LastName = customerEntity.LastName,
                Email = customerEntity.Email
            };

        }
    }

 
}


