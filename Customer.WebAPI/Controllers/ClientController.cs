using Customer.Core.Interfaces.IAppServices;
using Customer.DTOs.AppDTOs.Client;
using Customer.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace Customer.WebAPI.Controllers
{
    [Route("api/" + nameof(Contexts.CustomerAppContext) + "/" + "[controller]")]
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService customerService )
        {
            _clientService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries(PaginatedFiltration filtrationDTO)
        {

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (ModelState.IsValid)
            {
                return Ok(await _clientService.FilterByAsync(filtrationDTO));
            }

            return BadRequest(ModelState);

        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDTO clientCreateDto)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (ModelState.IsValid)
            {

                return Ok(await _clientService.CreateAsync(clientCreateDto));
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDCustomer([FromBody] ClientUpdateDTO clientUpdateDto)
        {


            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (ModelState.IsValid)
            {
                return Ok(await _clientService.UpdateAsync(clientUpdateDto));
            }

            return BadRequest(ModelState);

        }

    

        [HttpGet("GetClientById/{id}")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (ModelState.IsValid)
            {
                return Ok(await _clientService.GetByIdAsync(id));
            }

            return BadRequest(ModelState);

        }

        [HttpDelete("DeleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            if (ModelState.IsValid)
            {
                return Ok(await _clientService.DeleteAsync(id));
            }

            return BadRequest(ModelState);

        }

       
    }
}