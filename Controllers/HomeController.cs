using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    //__________Category_View______  
    [HttpGet("category")]
    public IActionResult Category()
    {
        ViewBag.All_Categories = _context.Categories.ToList();
        return View();
    }
    //___________Add a Category_____
    [HttpPost("/category/add")]
    public IActionResult CategoryAdd(Category newCategory)
    {
        if(ModelState.IsValid)
        {
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("category");
        }
        return View("category");
    }
    //__________One_Product________________
    [HttpGet("/product/{ProductId}")]
    public IActionResult OneProduct(int ProductId)
    {
        ViewBag.One_Product = _context.Products.SingleOrDefault(p=>p.ProductId == ProductId);
        ViewBag.ProductHasCategory = _context.Products
                                    .Include(p=>p.ProductBelong)
                                    .ThenInclude(a=>a.Category)
                                    .FirstOrDefault(p=>p.ProductId==ProductId);
        ViewData["All_Category"] = _context.Categories.ToList();
        return View();
    }
    //____________One_Category______________
    [HttpGet("/category/{CategoryId}")]
    public IActionResult OneCategory(int CategoryId)
    {
        ViewBag.One_Category = _context.Categories.FirstOrDefault(c=>c.CategoryId==CategoryId);
        ViewBag.CategoryHasProduct = _context.Categories
                                    .Include(c=>c.CategoryHave)
                                    .ThenInclude(a=>a.Product)
                                    .FirstOrDefault(p=>p.CategoryId==CategoryId);
        ViewData["All_Product"] = _context.Products.ToList();
        return View();
    }
    //____________Add_Association___________  
    [HttpPost("/association")]
    public IActionResult Association(Association newAssociation)
    {
            _context.Associations.Add(newAssociation);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
