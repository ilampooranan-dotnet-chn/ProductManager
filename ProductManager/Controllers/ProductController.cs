using Microsoft.AspNetCore.Mvc;
using ProductManager.Models;

namespace ProductManager.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200.00m },
            new Product { Id = 2, Name = "Mouse", Price = 25.50m },
            new Product { Id = 3, Name = "Monitor", Price = 350.00m }
        };
        private static int nextId = 4;
        public IActionResult Index()
        {
            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create (Saves the submitted product)
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against Cross-Site Request Forgery
        public IActionResult Create([Bind("Name,Price")] Product newProduct)
        {
            if (ModelState.IsValid)
            {
                newProduct.Id = nextId++;
                products.Add(newProduct);
                return RedirectToAction(nameof(Index)); // Redirects to the list view
            }
            return View(newProduct); // If validation fails, show the form again
        }

        // -----------------------------------------------------------
        // 4. UPDATE (UpdateProductPrice)
        // -----------------------------------------------------------
        // GET: Product/Edit/5 (Shows the form pre-filled with the current data)
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5 (Updates the product data)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Price")] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingProduct = products.FirstOrDefault(p => p.Id == id);
                if (existingProduct != null)
                {
                    // Update the properties
                    existingProduct.Name = updatedProduct.Name;
                    existingProduct.Price = updatedProduct.Price;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updatedProduct);
        }

        // -----------------------------------------------------------
        // 5. DELETE (DeleteProduct)
        // -----------------------------------------------------------
        // GET: Product/Delete/5 (Shows the confirmation page)
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5 (Performs the actual deletion)
        [HttpPost, ActionName("Delete")] // We name the action "DeleteConfirmed" to avoid conflict with GET
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var productToRemove = products.FirstOrDefault(p => p.Id == id);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
            }
            return RedirectToAction(nameof(Index));
        }
        // Helper method to allow other controllers (like Home) to access the list
        public static List<Product> GetProducts()
        {
            return products;
        }
    }
}
