using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Models
{
    public class Product
    {
        private decimal unitPrice;

        [Required]
        public string Description { get; set; }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.00, 9999.99, ErrorMessage = "Unit price must be between 0.00 and 9,999.99")]
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set
            {
                if (value < 0.0m)
                    throw new ArgumentException("Value cannot be less than 0.", "value");

                unitPrice = value;
            }
        }
    }
}