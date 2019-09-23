using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebServer.Models
{
    [Table("Product")]
    public class Product
    {
        [Key, Column("id")]
        public int ID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("price")]
        public double Price { get; set; }
    }

    public class FakeData{
        public static IDictionary<int, Product> Products;
        static FakeData(){
            Products = new Dictionary<int, Product>();
            Products.Add(0, new Product{ID=0, Name="Apple",Price = 5.55});
            Products.Add(1, new Product{ID=1, Name="Bike", Price = 6.66});
            Products.Add(2, new Product{ID=2, Name="Coffee",Price = 7.77});


        }
    }
}