using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Bank.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IRepositoriesHandler repositoriesHandler;
        private readonly IServiceHandler serviceHandler;
        private readonly IResourcesProvider resourcesProvider;

        public CustomerController(IRepositoriesHandler repositoriesHandler, IServiceHandler serviceHandler, IResourcesProvider resourcesProvider)
        {
            this.repositoriesHandler = repositoriesHandler;
            this.serviceHandler = serviceHandler;
            this.resourcesProvider = resourcesProvider;
        }

        [HttpPost]
        public IHttpActionResult AddCustomer([FromBody] Customer newCustomer)
        {
            var result = this.serviceHandler.CustomerService.CreateNewCustomer(newCustomer, this.repositoriesHandler, this.serviceHandler.PasswordService, this.resourcesProvider);
            if (result.Success)
            {
                return Ok(result.SuccessMessage);
            }
            else
            {
                return Content(HttpStatusCode.ExpectationFailed, result.Error);
            }
        }

        [HttpPost]
        public IHttpActionResult EditCustomer([FromBody] EditedCustomer editedCustomer)
        {
            var result = this.serviceHandler.CustomerService.UpdateCustomer(editedCustomer, this.repositoriesHandler, this.serviceHandler.PasswordService, this.resourcesProvider);
            if (result.Success)
            {
                return Ok(result.SuccessMessage);
            }
            else
            {
                return Content(HttpStatusCode.ExpectationFailed, result.Error);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllCustomers()
        {
            var customers = this.repositoriesHandler.СustomerRepository.GetCollection();
            return Ok(customers);
        }

        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            if (id > 0)
            {
                var customer = this.repositoriesHandler.СustomerRepository.GetById(id);
                return Ok(customer);
            }
            else
            {
                var error = this.resourcesProvider.GetGeneralResource("CusIdNull");
                return Content(HttpStatusCode.ExpectationFailed, error);
            }
        }
    }
}

