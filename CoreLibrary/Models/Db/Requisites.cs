using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLibrary.Models.Db
{
	public class Requisites
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequisitesId { get; set; }
		public string LowName { get; set; }
		public string LowAddress { get; set; }
		public int ZipCode { get; set; }
        public int? CityId { get; set; }
		public string SingleRegId { get; set; }
		public bool IsTaxesPayer { get; set; }
		public int TaxationId { get; set; }
		public string ContactPersonName { get; set; }

        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
    }
}

