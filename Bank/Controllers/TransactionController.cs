using Bank.Domain.Handlers.Abstract;
using Bank.Domain.Models;
using Bank.Domain.Providers;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bank.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly IRepositoriesHandler repositoriesHandler;
        private readonly IServiceHandler serviceHandler;
        private readonly IResourcesProvider resourcesProvider;

        public TransactionController(IRepositoriesHandler repositoriesHandler, IServiceHandler serviceHandler, IResourcesProvider resourcesProvider)
        {
            this.repositoriesHandler = repositoriesHandler;
            this.serviceHandler = serviceHandler;
            this.resourcesProvider = resourcesProvider;
        }

        [HttpPost]
        public async Task<IHttpActionResult> PerformTransaction([FromBody] Transaction newTransaction)
        {
            var result = await this.serviceHandler.TransactionService.PerformTransactionAsync(newTransaction, this.repositoriesHandler, resourcesProvider);
            if (result.Success)
            {
                return Ok(result.SuccessMessage);
            }

            return Content(HttpStatusCode.InternalServerError, result.Error);
        }

        [HttpPost]
        public IHttpActionResult GetTransactionsByCard([FromBody] SearchFilter filter)
        {
            if (filter.CreditCardID > 0)
            {
                var transactions = this.repositoriesHandler.TransactionRepository.GetTransactionsByCard(filter);
                return Ok(transactions);
            }
            else
            {
                var error = this.resourcesProvider.GetGeneralResource("CCIdNull");
                return Content(HttpStatusCode.ExpectationFailed, error);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> GenerateCardRandomTransactions(int toCardID)
        {
            var result = await this.serviceHandler.TransactionService.GenerateCardRandomTransactionsAsync(toCardID, this.repositoriesHandler, this.resourcesProvider);
            if(result.Success)
            {
                return Ok(result.SuccessMessage);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result.Error);
            }
        }
    }
}
