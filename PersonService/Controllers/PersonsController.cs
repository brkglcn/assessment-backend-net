using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;

namespace PhoneBook.PersonService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _personService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _personService.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PersonCreateDto dto)
    {
        var id = await _personService.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _personService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("contact")]
    public async Task<IActionResult> AddContact([FromBody] ContactCreateDto dto)
    {
        await _personService.AddContactAsync(dto);
        return Ok();
    }

    [HttpDelete("contact/{contactId}")]
    public async Task<IActionResult> RemoveContact(Guid contactId)
    {
        await _personService.RemoveContactAsync(contactId);
        return NoContent();
    }
}