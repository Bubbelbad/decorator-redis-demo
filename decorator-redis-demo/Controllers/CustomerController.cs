using DecoratorRedisDemo.Database;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace DecoratorRedisDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CustomerController : ControllerBase
    {
        public CustomerController()
        {
        }

        [HttpGet(Name = "Get")]
        public static IEnumerable<CustomerEntity> Get()
        {
			throw new NotImplementedException(); 
        }
    }
}
