#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ProductsAndCategories.Models;  
public class Category
{
    [Key]
    public int CategoryId {get;set;}
    [Required]
    [MinLength(3)]
    public string Name {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt{get;set;} = DateTime.Now;

    public List<Association> CategoryHave {get;set;} = new List<Association>();

}