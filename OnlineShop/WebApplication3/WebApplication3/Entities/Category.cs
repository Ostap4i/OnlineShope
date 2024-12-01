using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication3.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CcategoryId { get; set; }
        
    }
}
