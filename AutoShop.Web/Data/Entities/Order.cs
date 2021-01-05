using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AutoShop.Web.Data.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime OrderDate { get; set; }


        [Display(Name = "Delivery date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime? DeliveryDate { get; set; }

        public Client Client { get; set; }

        [Required]
        public int ClientId {get; set;}

        public ClientVehicle ClientVehicle { get; set; }

        [Required]
        public int ClientVehicleId { get; set; }

        public IEnumerable<Services_Orders> Items { get; set; }


        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines { get { return this.Items == null ? 0 : this.Items.Count(); } }


        public int Quantity { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Qtd); } }


        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Service.Price * i.Qtd); } }

        [Display(Name = "Order date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? OrderDateLocal
        {
            get
            {
                if (this.OrderDate == null)
                {
                    return null;
                }

                return this.OrderDate.ToLocalTime();
            }
        }

        public bool IsAproved { get; set; }
    }
}
