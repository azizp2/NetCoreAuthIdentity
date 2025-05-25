using api.Models;
using api.Services;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/Invoice")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly InvoiceRenderingService _invServices;
        public InvoiceController(InvoiceRenderingService invServices)
        {
            _invServices = invServices;
        }

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

        [HttpGet("/GeneratedPdf")]
        public ActionResult GeneratedPdf()
        {
            var invoice = new Faker<Invoice>("id_ID")
                .RuleFor(i => i.InvoiceDate, f => f.Date.Recent(30))
                .RuleFor(i => i.InvoiceNumber, f => f.Random.Number(100000, 999999).ToString())
                .Generate();


            invoice.Client = new Faker<Client>("id_ID")
                .RuleFor(c => c.ClientName, f => f.Company.CompanyName().ToLower())
                .RuleFor(c => c.ClientAddress, f => f.Address.FullAddress())
                .Generate();

            invoice.InvoiceItems = new ();
            for (var i = 1; i <= 10; i++)
            {
                invoice.InvoiceItems.Add(new Faker<InvoiceItem>("id_ID")
                    .RuleFor(i => i.Description, f => f.Commerce.ProductName())
                    .RuleFor(i => i.Quantity, f => f.Random.Int(1, 9))
                    .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price()))
                    .Generate());
            }

            var document = _invServices.GeneratedInvoicePdf(invoice);



            return File(document, "application/pdf", "Invoice.pdf");
        }
     }
}
