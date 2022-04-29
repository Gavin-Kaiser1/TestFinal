using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestFinal.Models
{
    public class Inventory
    {
        //This class is for the database and scafolding, its a model 
        //unique Identifier
        [Required]
        public int Id { get; set; }
        //This is for the naimg of the items
        [Display(Name = "Item Name")]
        [Required]
        public string Name { get; set; } = string.Empty;
        //This is for the weight of athe pallets
        [Display(Name ="Weight in LBS per Pallet")]
        [Required]
        public int Weight { get; set; }
        //This is for the description of the item
        [Required]
        public string Description { get; set; } = string.Empty;
        //This is for the amount of pallets in the item
        [Display(Name = "Number of Pallets")]
        [Required]
        public int ItemCount { get; set; }
        //This is the set amount fot the minimum of allowed items before a warning
        [Required]
        public int MinAlertThresh { get; set; }
        //This is for the most recent day that an item was modified
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Time Modified")]
        public DateTime TimeArrived { get; set; }
        
    }

}
