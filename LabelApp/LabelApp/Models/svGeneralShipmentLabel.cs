using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Models
{
    public class svGeneralShipmentLabel
    {
        private DateTime shipmentDate = DateTime.MinValue;
        [Key]
        public int ShipmentID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(50)]
        [RegularExpression("[A-Za-z ]*", ErrorMessage = "Use letters and space only")]
        public string FromName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string FromAddress1 { get; set; }
        [Display(Name = "Address 2")]
        [StringLength(100)]
        public string FromAddress2 { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string FromCity { get; set; }
        [Required]
        [StringLength(2)]
        [Display(Name = "State")]
        public string FromState { get; set; }
        [Required]
        [Display(Name = "Zip")]
        [StringLength(5)]
        public string FromZip { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string FromPhone { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(50)]
        public string ToName { get; set; }

        [Display(Name = "Company")]
        [StringLength(50)]
        public string ToCompany { get; set; }
        [Required]
        [Display(Name = "Address 1")]
        [StringLength(100)]
        public string ToAddress1 { get; set; }
        [Display(Name = "Address 2")]
        [StringLength(100)]
        public string ToAddress2 { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "City")]
        public string ToCity { get; set; }
        [Required]
        [Display(Name = "State")]
        [StringLength(2)]
        public string ToState { get; set; }
        [Required]
        [StringLength(5)]
        [Display(Name = "Zip")]
        public string ToZip  { get; set; }
        [Required]
        [Phone]
        [StringLength(10)]
        [Display(Name = "Phone")]
        public string ToPhone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(100)]        
        public string ToEmail { get; set; }
        [Required]
        [Display(Name = "Ship Via")]
        public string ShipVia { get; set; }
        [Required]
        [Display(Name = "Ship Package")]
        public string ShipPackage { get; set; }
        [Required]
        [Display(Name = "Carrier")]
        public string Carrier { get; set; }

        public string TrackingNumber { get; set; }
        [Required]
        [Display(Name = "Shipment date")]
        //ataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime RequestedShipmentdate { get; set; }
        //{
        //    get
        //    {
        //        return shipmentDate;// (shipmentDate == DateTime.MinValue) ? DateTime.Now : shipmentDate;
        //    }
        //    set
        //    {
        //        shipmentDate = value;
        //    }
        //}
        public decimal FinalPostage { get; set; }
        [Required]
        [Display(Name = "Shipment weight")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal ShipmentWeight { get; set; }
        public string ShippingLabel { get; set; }
        [Display(Name = "Internation only")]
        public bool IsInternation { get; set; }
        public string LabelStatus { get; set; }
        
    }
}
