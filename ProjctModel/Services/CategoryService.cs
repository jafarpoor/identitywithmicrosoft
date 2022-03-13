using ProjctModel.Context;
using ProjctModel.Entites;
using ProjctModel.VeiwModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjctModel.Services
{
   public class CategoryService
    {
        private readonly DataBaseContext context;
        public CategoryService(DataBaseContext dataBaseContext)
        {
            context = dataBaseContext;
        }

        public bool AddCategory(CategoryDto categoryDto)
        {
            Category category = new Category
            {
                Title = categoryDto.Title
            };
            context.Add(category);
            context.SaveChanges();
            return true;
        }

        public List<CategoryDto> ListCategory()
        {
            var Resulte = context.Categories.Select(p => new CategoryDto
            {
                Title = p.Title
            }).ToList();

            return Resulte;
        }
    }
}
