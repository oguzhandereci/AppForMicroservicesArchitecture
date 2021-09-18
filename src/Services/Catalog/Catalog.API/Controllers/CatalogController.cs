using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Utilities.Extensions;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetProducts();

            return Ok(products);
        }

        [HttpGet("{Id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string Id)
        {
            Product prd = await _productRepository.GetProduct(Id);

            return !(prd is null) ? prd : NotFound($"{Id}' li herhangi bir ürün bulunamadı.");
        }

        //GetProductsByCategoryName
        [HttpGet("/[action]/{catName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string catName)
        {
            IEnumerable<Product> productsOfSpecifiedCategory = await _productRepository.GetProductByCategory(catName);
            return productsOfSpecifiedCategory.Any() ? Ok(productsOfSpecifiedCategory) : NotFound($"{catName} isimli kategoriye ait herhangi bir ürün bulunamadı.");
        }
        //Insert
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> InsertProduct([FromBody] Product product)
        {
            return Ok(_productRepository.CreateProduct(product));
        }
        //Update
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            bool updateResult = await _productRepository.UpdateProduct(product);
            return updateResult ? Ok($"{product.Id}' li ürün başarıyla güncellenmiştir.") : this.Fail();
        }

        //Delete
        [HttpDelete("{Id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<ActionResult> DeleteProduct(string Id)
        {
            bool deleteResult = await _productRepository.DeleteProduct(Id);
            return deleteResult ? Ok($"{Id}' li ürün başarıyla silinmiştir.") : this.Fail();
        }
    }
}
