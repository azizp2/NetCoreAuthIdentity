using api.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/Dummy")]
    [ApiController]
    public class DummyController : Controller
    {
        [HttpGet] 
        public ActionResult GetDataDumy() {
            var faker = new Faker<Customers>("id_ID")
                .RuleFor(c => c.Id, f => f.Random.Int(1,1000))
                .RuleFor(c => c.FullName, f => f.Company.CompanyName())
                .RuleFor(c => c.Email, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Email, f => f.Internet.Email());


            var fakerCustomer = faker.Generate(100);
            return Ok(fakerCustomer);
        }
     }
}
