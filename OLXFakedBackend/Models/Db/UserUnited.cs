using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace OLXFakedBackend.Models.Db
{
	public class UserUnited
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitedUserId { get; set; }

        public string? UserId { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public Image? Image { get; set; }
        public Requisites? Requisites { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser? User { get; set; }
    }
}

