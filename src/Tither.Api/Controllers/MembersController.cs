using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MongoDB.Bson;
using Tither.Domain.Models;
using Tither.Shared.Requests;

namespace Tither.Api.Controllers
{
    [EnableQuery]
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : TitherControllerBase
    {
        public MembersController(IMediator mediator)
            : base(mediator) { }

        [HttpGet("getAll")]
        public Task<IActionResult> GetAll()
            => SendRequest(new GetAllMembersRequest());

        [HttpPost("register")]
        public Task<IActionResult> Register([FromBody] RegisterNewMemberRequest member)
            => SendRequest(member);

        [HttpPatch("update")]
        public Task<IActionResult> Update([FromBody] UpdateMemberRequest member)
            => SendRequest(member);
    }
}