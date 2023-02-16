using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Data;
using MyWebAPI.Models;
using System.Collections.Generic;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("CategorySearch/{name}")]
        public  async Task <ActionResult> GETcategory(string name)
        {
            //var hero = _db.Products.Where(c => name.Contains(c.Name)).Include(c => c.Category).ToList();
            var categoryList = from t1 in _db.Categories join t2 in _db.Products on t1.CategoryId equals t2.CategoryId where t1.Name == name group t2.Name by t1.Name into aa select new {Category = aa.Key,Products=string.Join(',',aa.ToArray())};
            if (categoryList == null)
                return BadRequest("Hero not Found");
            return Ok(categoryList);
        }

        [HttpGet]
        [Route("ProductSearch/{name}")]
        public async Task<ActionResult> GETproduct(string name)
        {
            //var hero =  _db.Products.Where(c => name.Contains(c.Category.Name)).Include(c => c.Category).ToList();
            //var productList = from t1 in _db.Products join t2 in _db.Categories on t1.Category.Name equals t2.Name where t1.Name==name select (new{t1,t2.Name });
            var P1 = (from t1 in _db.Products join t2 in _db.Categories on t1.CategoryId equals t2.CategoryId where t1.Name == name group t1 by t2 into aa select new {Product = aa.Key , Category = aa }).ToList();
            if (P1.Count == 0)
                return BadRequest("Record not Found");
            return Ok(P1);
        }

    }
}
