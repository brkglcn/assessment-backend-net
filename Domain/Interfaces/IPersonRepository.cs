using PhoneBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Domain.Interfaces;

public interface IPersonRepository
{
    Task<List<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(Guid id);
    Task AddAsync(Person person);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}