using Microsoft.AspNetCore.Mvc;
using ProductCRUD.Models;
using ProductCRUD.Repositories;

namespace ProductCRUD.Controllers
{
        public class ProductController : Controller
        {
            private readonly IProductRepository _productRepository;

            public ProductController(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<IActionResult> Index()
            {
                var products = await _productRepository.GetAllProductsAsync();
                return View(products);
            }

            public IActionResult Create() => View();

            [HttpPost]
            public async Task<IActionResult> Create(Product product)
            {
                if (ModelState.IsValid)
                {
                    await _productRepository.AddProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }

            public async Task<IActionResult> Edit(int id)
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(Product product)
            {
                if (ModelState.IsValid)
                {
                    await _productRepository.UpdateProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }

            public async Task<IActionResult> Delete(int id)
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }

            [HttpPost, ActionName("Delete")]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _productRepository.DeleteProductAsync(id);
                return RedirectToAction(nameof(Index));
            }


            public async Task<IActionResult> Details(int id)
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }
}
