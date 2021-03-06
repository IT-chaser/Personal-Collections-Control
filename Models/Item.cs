using System.ComponentModel.DataAnnotations;

namespace CollectionsApp.Models;

public class Item {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Tags { get; set; }
    public string? Topic { get; set; }
    public virtual Collection? Collection { get; set; }
    
}