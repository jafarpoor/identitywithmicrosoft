using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjctModel.Services;
using ProjctModel.VeiwModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExJwt2.Controllers
{
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize("admin")]
        [HttpPost]
        [Route("Post")]
        public IActionResult Post(CategoryDto categoryDto)
        {
            var Result = _categoryService.AddCategory(categoryDto);
            return Ok(Result);
        }

       // [Authorize]
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            return Ok(new List<string> { "1" , "2" ,"3"});
        }
    }
}
