using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bank.Controllers
{
    public class CreditCardController : ApiController
    {
        private readonly IRepositoriesHandler repositoriesHandler;
        private readonly IServiceHandler serviceHandler;
        private readonly IResourcesProvider resourcesProvider;

        public CreditCardController(IRepositoriesHandler repositoriesHandler, IServiceHandler serviceHandler, IResourcesProvider resourcesProvider)
        {
            this.repositoriesHandler = repositoriesHandler;
            this.serviceHandler = serviceHandler;
            this.resourcesProvider = resourcesProvider;
        }

        [HttpPost]
        public IHttpActionResult AddCreditCard([FromBody] CreditCard newCreditCard)
        {
            var result = this.serviceHandler.CreditCardService.CreateNewCreditCard(newCreditCard, this.repositoriesHandler, this.serviceHandler.PasswordService, this.resourcesProvider);
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
        public IHttpActionResult EditCreditCard([FromBody] EditedCreditCard editedCreditCard)
        {
            var result = this.serviceHandler.CreditCardService.UpdateCreditCard(editedCreditCard, this.repositoriesHandler, this.serviceHandler.PasswordService, this.resourcesProvider);
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
        public IHttpActionResult GetAllCreditCards()
        {
            var cards = this.repositoriesHandler.СreditCardRepository.GetCollection();
            return Ok(cards);
        }

        [HttpGet]
        public IHttpActionResult GetCreditCard(int creditCardID)
        {
            if (creditCardID > 0)
            {
                var creditCard = this.repositoriesHandler.СreditCardRepository.GetById(creditCardID);
                return Ok(creditCard);
            }
            else
            {
                var error = this.resourcesProvider.GetGeneralResource("CCIdNull");
                return Content(HttpStatusCode.ExpectationFailed, error);
            }
        }

        [HttpGet]
        public IHttpActionResult GetCustomerCreditCards(int customerID)
        {
            if (customerID > 0)
            {
                var cards = this.repositoriesHandler.СreditCardRepository.GetByCustomerId(customerID);
                return Ok(cards);
            }
            else
            {
                var error = this.resourcesProvider.GetGeneralResource("CusIdNull");
                return Content(HttpStatusCode.ExpectationFailed, error);
            }
        }
    }
}
