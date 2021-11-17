using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticalApp.Class;
using PracticalApp.DataObject;

namespace PracticalApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ISettings options;

        public ProductController(ISettings options)
        {
            this.options = options;            
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                ProductDataObject clsproduct = new ProductDataObject();
                var product = clsproduct.GetAll();

                return Ok(new { IsError = 0, Message = "Success", data = product });
            }
            catch (Exception ex)
            {
                return Ok(new { IsError = 1, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product productObjectData)
        {
            try
            {
                ProductDataObject clsproduct = new ProductDataObject();
                Model.Product product = new Model.Product();
                product.ProductName = productObjectData.ProductName;
                product.Quantity = productObjectData.Quantity;
                product.Price = productObjectData.Price;
                product.isDeleted = false;

                long result = clsproduct.Insert(product);

                if (result > 0)
                {
                    return Ok(new { IsError = 0, message = "Data Saved Successfully." });
                }
                else
                {
                    return Ok(new { IsError = 1, message = "Data can not save successfully." });
                }
            }
            catch(Exception ex)
            {
                return Ok(new { IsError = 1, message = ex.Message  });
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product productObjectData)
        {
            try
            {
                ProductDataObject clsproduct = new ProductDataObject();
                Model.Product product = new Model.Product();
                product.ProductId = productObjectData.ProductId;
                product.ProductName = productObjectData.ProductName;
                product.Quantity = productObjectData.Quantity;
                product.Price = productObjectData.Price;
                product.isDeleted = false;

                long result = clsproduct.Update(product);

                if (result > 0)
                {
                    return Ok(new { IsError = 0, message = "Data Updated Successfully." });
                }
                else
                {
                    return Ok(new { IsError = 1, message = "Data can not update successfully." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { IsError = 1, message = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult Delete(long ProductId)
        {
            try
            {
                ProductDataObject clsproduct = new ProductDataObject();              

                long result = clsproduct.Delete(ProductId);

                if (result > 0)
                {
                    return Ok(new { IsError = 0, message = "Data Deleted Successfully." });
                }
                else
                {
                    return Ok(new { IsError = 1, message = "Data can not delete successfully." });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { IsError = 1, message = ex.Message });
            }
        }
    }    
}
