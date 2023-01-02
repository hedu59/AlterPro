using MessageConsumer.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MessageConsumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly ITransacaoMongoRepository _repository;
        public InvitationController(ITransacaoMongoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetInvitatiations()
        => Ok(await _repository.GetAllAsync());

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetInvitatiationById(string Id)
        => Ok(await _repository.GetByIdAsync(Id));
    }
}
