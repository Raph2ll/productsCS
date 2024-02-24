using System;
using System.ComponentModel.DataAnnotations;


namespace api.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Numbers and special characters are not allowed in the name.")]
        public required string Name { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Price field must be greater than 0.")]
        public double Price { get; set; }
    }

}