using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context; 

    public HomeController(ILogger<HomeController> logger,MyContext context)
    {
        _logger = logger;
        _context= context;
    }

    public IActionResult Index()
    {
        //getting all the product
        ViewBag.All_Products = _context.Products.ToList();
        return View();
    }
    //________Add a Product______
    [HttpPost("/product/add")]
    public IActionResult ProductAdd(Product newProduct)
    {
        if(ModelState.IsValid)
        {
        _context.Products.Add(newProduct);
        _context.SaveChanges();
        return RedirectToAction("Index");
        }
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
