using System;
using System.ComponentModel.DataAnnotations;


namespace api.Model
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public double Price { get; set; }
    }

}