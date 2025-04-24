using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;
using PhoneBook.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PhoneBookDbContext _context;

    public PersonRepository(PhoneBookDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
    }

    public async Task DeleteAsync(Guid id)
    {
        var person = await _context.Persons.Include(p => p.ContactInfos).FirstOrDefaultAsync(p => p.Id == id);
        if (person != null)
        {
            _context.ContactInfos.RemoveRange(person.ContactInfos);
            _context.Persons.Remove(person);
        }
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.Persons.Include(p => p.ContactInfos).ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _context.Persons.Include(p => p.ContactInfos).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}