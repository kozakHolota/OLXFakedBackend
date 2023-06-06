using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLibrary.Models.Db
{
	public class ContactPerson
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactPersonId { get; set; }
		public string Name { get; set; }
        public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
    }
}

