using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTCSDL_IM91_EF.Models;

namespace LTCSDL_IM91_EF.Pages
{
    public class IndexModel : PageModel
    {
        public List<Category> lst;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            /*// Them
            Category c = new Category();
            c.CategoryName = "EF";
            c.Description = "Entity Framework";
            var cat = AddNewCategory(c);
            */
            /*//Update
            Category c = new Category();
            c.CategoryId = 13;
            c.CategoryName = "LT-EF";
            c.Description = "Lap Trinh Entity Framework";
            var res = UpdateCategory(c);
            */
             //Xoa
            var res = DeleteCategory(13);
            
            lst = LoadCategory().ToList();
        }

        public IList<Category> LoadCategory()
        {
            IList<Category> lst = new List<Category>();
            using (var db = new NorthwindContext())
            {
                var cate = from c in db.Categories
                           select c;
                lst = cate.ToList();
            }
            return lst;
        }

        public Category AddNewCategory(Category c)
        {
            using (var db = new NorthwindContext())
            {
                db.Categories.Add(c);
                db.SaveChanges();
            }
            return c;
        }

        public bool UpdateCategory(Category c)
        {
            bool res = false;
            using (var db = new NorthwindContext())
            {
                Category cat = db.Categories.First(x => x.CategoryId == c.CategoryId);
                if (string.IsNullOrEmpty(c.CategoryName) == false)
                    cat.CategoryName = c.CategoryName;
                if (string.IsNullOrEmpty(c.Description) == false)
                    cat.Description = c.Description;
                db.SaveChanges();
                res = true;
            }
            return res;
        }

        public bool DeleteCategory(int id)
        {
            bool res = false;
            using (var db = new NorthwindContext())
            {
                Category c = db.Categories.First(x => x.CategoryId == id);
                db.Categories.Remove(c);
                db.SaveChanges();
                res = true;
            }
            return res;
        }
    }
}
