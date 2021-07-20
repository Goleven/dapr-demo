using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using OrderService.Dto;

namespace OrderService.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<OrderController> _logger;

        public OrderController(DaprClient daprClient, ILogger<OrderController> logger)
        {
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("order-list")]
        [HttpGet]
        public async Task<List<OrderDto>> GetOrderList()
        {
            try
            {
                Random r = new Random();
                if (r.Next(1, 5) % 2 == 0)
                {
                    throw new NotSupportedException("error");
                }

                Faker<OrderDto> faker = new Faker<OrderDto>()
                    .RuleFor(x => x.OrderID, _ => Guid.NewGuid())
                    .RuleFor(x => x.Address, f => f.Address.FullAddress())
                    .RuleFor(x => x.CreateTime, f => f.Date.Past())
                    .RuleFor(x => x.ProductCount, f => f.Random.Int(1, 50));

                _logger.LogInformation($"from order: {DateTime.Now.ToString("HH:mm:ss fff")}");
                _logger.LogInformation($"order port: [{Request.Host}]");

                return await Task.Run(() => { return faker.Generate(20); });
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

                _logger.LogInformation($"from order: {DateTime.Now.ToString("HH:mm:ss fff")}");
                _logger.LogInformation($"order port: [{Request.Host}]");

                return await _daprClient.InvokeMethodAsync<ProductDto>(HttpMethod.Get, "service-product",
                    "product/product");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [Route("publish")]
        public async Task PublishEvent()
        {
            ProductDto dto = new ProductDto()
            {
                ProductID = Guid.NewGuid(),
                Price = 0,
                ProductName = "product",
                SupplerName = "supplier"
            };
            _logger.LogInformation($"publisher：{JsonSerializer.Serialize(dto)}");
            await _daprClient.PublishEventAsync<ProductDto>("pubsub", "set-val", dto);
        }
    }
}
