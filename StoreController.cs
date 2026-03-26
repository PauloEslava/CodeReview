using Microsoft.AspNetCore.Mvc;
using AppWebAWAQ.Models;

namespace AppWebAWAQ.Controllers
{
    public class StoreController : Controller
    {
        private static int userCoins = 1000; // Monedas iniciales del usuario
        private static List<int> purchasedItems = new List<int>(); 

        public IActionResult Store(string searchQuery) // Acción para mostrar la tienda
            {
                var products = GetProducts(); 

                foreach (var product in products) // Marcar los productos comprados como no disponibles
                {
                    if (purchasedItems.Contains(product.Id))
                    {
                        product.IsAvailable = false; 
                    }
                
                    if(product.Id == 1)
                    {
                        product.IsUnlocked = true;
                    }
                    else if (product.Id == 2)
                    {
                        product.IsUnlocked = purchasedItems.Contains(1);
                    }
                    else if (product.Id == 3)
                    {
                        product.IsUnlocked = purchasedItems.Contains(2);
                    }
                    else if (product.Id == 4)
                    {
                        product.IsUnlocked = purchasedItems.Contains(3);
                    }
                    else
                    {
                        product.IsUnlocked = true;
                    }

                }

                if (!string.IsNullOrWhiteSpace(searchQuery)) // Filtrar productos por nombre, ignorando mayúsculas y minúsculas
                {
                    products = products
                        .Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                var model = new StoreViewModel // Crear el modelo para la vista
                {
                    Products = products,
                    SearchQuery = searchQuery,
                    UserCoins = userCoins
                };

                return View(model);
            }

            [HttpPost]
            public IActionResult Purchase(int productId)
            {
                var products = GetProducts();
                var product = products.FirstOrDefault(p => p.Id == productId); //buscar el producto por su ID

                if (product == null) //si el producto no existe, regresa a la tienda
                {
                    return RedirectToAction("Store");
                }

                bool isUnlocked = false;
                if(product.Id == 1)
                {
                    isUnlocked = true;
                }
                else if (product.Id == 2)
                {
                    isUnlocked = purchasedItems.Contains(1);
                }
                else if (product.Id == 3)
                {
                    isUnlocked = purchasedItems.Contains(2);
                }
                else if (product.Id == 4)
                {
                    isUnlocked = purchasedItems.Contains(3);
                }
                else                {
                    isUnlocked = true;
                }

                if (product.IsAvailable && userCoins >= product.Price)
                {
                    userCoins -= product.Price; 
                    purchasedItems.Add(product.Id); 
                }

                return RedirectToAction("Store");
            }
        private List<Product> GetProducts() // Método para obtener la lista de productos disponibles en la tienda
        {
            return new List<Product> 
            {
                new Product { Id = 1, Name = "Mejora 1", Description = "Primera mejora, se puede obtener al completar un mini juego.", Price = 500, ImageUrl = "/images/store/mejora1.png"},
                new Product { Id = 2, Name = "Mejora 2", Description = "Segunda mejora se puede obtener al completar 2 mini juegos.", Price = 300, ImageUrl = "/images/store/mejora2.png"},
                new Product { Id = 3, Name = "Mejora 3", Description = "Tercera mejora se puede obtener al completar 3 mini juegos.", Price = 100, ImageUrl = "/images/store/mejora3.png"},
                new Product { Id = 4, Name = "Mejora 4", Description = "Cuarta mejora se puede obtener al completar 4 mini juegos.", Price = 400, ImageUrl = "/images/store/mejora4.png"},
                new Product { Id = 5, Name = "Banca", Description = "Lugar para sentarse y descansar.", Price = 50, ImageUrl = "/images/store/banca.png"},
                new Product { Id = 6, Name = "Flores", Description = "Flores que añaden color al entorno.", Price = 20, ImageUrl = "/images/store/flowers.png"},
                new Product { Id = 7, Name = "Estación ambiental", Description = "Pequeña estación que forma parte del paisaje.", Price = 30, ImageUrl = "/images/store/psca.png" },
                new Product { Id = 8, Name = "Panel de especies", Description = "Panel que muestra especies del ecosistema.", Price = 40, ImageUrl = "/images/store/panel.png" }
            };
        }
    }
}