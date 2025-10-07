using Microsoft.AspNetCore.Mvc;
using ProductApp.Models; // Import the new ViewModel

namespace ProductApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index (Displays the search form)
        public IActionResult Index()
        {
            // Pass a new, empty model to the view for form binding
            return View(new ProductActionViewModel());
        }

        // POST: /Home/Index (Handles form submission and redirects)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ProductActionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // If ID or ActionName is missing, return the form with errors
                return View(model);
            }

            // --- Redirect to the appropriate action in ProductController ---

            // The ActionName property (e.g., "Details", "Edit", "Delete") 
            // is used to determine the destination.

            return RedirectToAction(
                actionName: model.ActionName,  // e.g., "Details", "Edit", or "Delete"
                controllerName: "Product",
                routeValues: new { id = model.Id } // Pass the user-supplied ID
            );
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}