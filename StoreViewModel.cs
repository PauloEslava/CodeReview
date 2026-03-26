
namespace AppWebAWAQ.Models
{
    public class StoreViewModel
    {
        public List<Product> Products { get; set; }
        public string SearchQuery { get; set; }
        public int UserCoins { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsUnlocked {get; set;} = false;
    }
    
}