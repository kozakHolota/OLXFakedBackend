using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLibrary.Models.Db
{
	public class ContactData
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactDataId { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }

    }
}

