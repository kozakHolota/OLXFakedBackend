using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLibrary.Models.Db
{
	public class City
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
		public string Name { get; set; }
		[Required]
		public int DistrictId { get; set; }

		public District District { get; set; }
	}
}

