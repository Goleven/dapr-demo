using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("product-list")]
        [HttpGet]
        public async Task<List<ProductDto>> GetProductList()
        {
            try
            {
                Random r = new Random();
                if (r.Next(1, 5) % 2 == 0)
                {
                    throw new NotSupportedException("error");
                }

                Faker<ProductDto> faker = new Faker<ProductDto>()
                    .RuleFor(x => x.ProductID, f => f.Random.Guid())
                    .RuleFor(x => x.ProductName, f => f.Random.String2(9))
                    .RuleFor(x => x.Price, f => f.Random.Decimal(50, 200))
                    .RuleFor(x => x.SupplerName, f => f.Company.CompanyName());

                _logger.LogInformation($"from product: {DateTime.Now.ToString("HH:mm:ss fff")}");
                _logger.LogInformation($"product port: [{Request.Host}]");

                return await Task.Run(() => { return faker.Generate(5); });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        [Route("product")]
        [HttpGet]
        public async Task<ProductDto> GetProduct()
        {
            try
            {
                Random r = new Random();
                if (r.Next(1, 5) % 2 == 0)
                {
                    throw new NotSupportedException("error");
                }

                Faker<ProductDto> faker = new Faker<ProductDto>()
                    .RuleFor(x => x.ProductID, f => f.Random.Guid())
                    .RuleFor(x => x.ProductName, f => f.Random.String2(9))
                    .RuleFor(x => x.Price, f => f.Random.Decimal(50, 200))
                    .RuleFor(x => x.SupplerName, f => f.Company.CompanyName());

                _logger.LogInformation($"from product: {DateTime.Now.ToString("HH:mm:ss fff")}");
                _logger.LogInformation($"product port: [{Request.Host}]");

                return await Task.Run(() => { return faker.Generate(); });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
