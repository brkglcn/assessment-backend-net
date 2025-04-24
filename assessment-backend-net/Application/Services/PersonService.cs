using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace PhoneBook.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Guid> CreateAsync(PersonCreateDto dto)
    {
        var person = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Company = dto.Company
        };

        await _personRepository.AddAsync(person);
        await _personRepository.SaveChangesAsync();

        return person.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _personRepository.DeleteAsync(id);
        await _personRepository.SaveChangesAsync();
    }

    public async Task<List<PersonDto>> GetAllAsync()
    {
        var persons = await _personRepository.GetAllAsync();

        return persons.Select(p => new PersonDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Company = p.Company
        }).ToList();
    }

    public async Task<PersonDetailDto?> GetByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null) return null;

        return new PersonDetailDto
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Company = person.Company,
            Contacts = person.ContactInfos.Select(ci => new ContactDto
            {
                Id = ci.Id,
                Type = ci.Type,
                Content = ci.Content
            }).ToList()
        };
    }

    public async Task AddContactAsync(ContactCreateDto dto)
    {
        var person = await _personRepository.GetByIdAsync(dto.PersonId);
        if (person == null) return;

        person.ContactInfos.Add(new ContactInfo
        {
            Id = Guid.NewGuid(),
            PersonId = dto.PersonId,
            Type = dto.Type,
            Content = dto.Content
        });

        await _personRepository.SaveChangesAsync();
    }

    public async Task RemoveContactAsync(Guid contactId)
    {
        var personList = await _personRepository.GetAllAsync();
        var person = personList.FirstOrDefault(p => p.ContactInfos.Any(c => c.Id == contactId));
        if (person == null) return;

        var contact = person.ContactInfos.FirstOrDefault(c => c.Id == contactId);
        if (contact != null)
        {
            person.ContactInfos.Remove(contact);
            await _personRepository.SaveChangesAsync();
        }
    }
}
