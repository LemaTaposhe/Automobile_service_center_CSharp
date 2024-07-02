using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository_Domain
{
    [Serializable]
    [Table("Car")]
    public class Car : IEntity<int>
    {
        [Key]
        public int CarID { get; set; }
        [Required]
        
        public string CarName { get; set; }
        [Required]
       
        public string LicensePlate { get; set; }
        [Required]
        
        public string CustomerName { get; set; }
        [Required]
        
        public string CellPhoneNo { get; set; }
    }
}
