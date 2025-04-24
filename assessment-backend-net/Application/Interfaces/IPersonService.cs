using PhoneBook.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Application.Interfaces;

public interface IPersonService
{
    Task<List<PersonDto>> GetAllAsync();
    Task<PersonDetailDto?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(PersonCreateDto dto);
    Task DeleteAsync(Guid id);
    Task AddContactAsync(ContactCreateDto dto);
    Task RemoveContactAsync(Guid contactId);
}