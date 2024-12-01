using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult Get()
        {
            var catagories = _dbContext.category.ToList();
            return Ok(catagories);
        }


        //get by id
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("Details")]
        public ActionResult Get(int id)
        {
            var catagories = _dbContext.category.FirstOrDefault(x => x.Id == id);

            if (catagories == null)
            {
                return NotFound($"Category not found for Id - {id}");
            }
            return Ok(catagories);
        }


        //create
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]

        [HttpPost]
        public ActionResult Create([FromBody] Category category)
        {
            _dbContext.category.Add(category);
            _dbContext.SaveChanges();
            return Ok();
        }


        //update
         [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]

        [HttpPut]
        public ActionResult Update([FromBody] Category category)
        {
            _dbContext.category.Update(category);
            _dbContext.SaveChanges();
            return NoContent();
        }

        //Delete All
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpDelete]
        [Route("All")]
        public ActionResult DeleteAll()
        {
            var AllCategory = _dbContext.category.ToList();
            _dbContext.category.RemoveRange(AllCategory);
             _dbContext.SaveChanges();
            return NoContent();
        }

         [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var category = _dbContext.category.FirstOrDefault(x=>x.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            _dbContext.category.Remove(category);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}