using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Application.Services;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;
using FluentAssertions;

public class PersonServiceTests
{
    private readonly Mock<IPersonRepository> _mockRepo;
    private readonly PersonService _service;

    public PersonServiceTests()
    {
        _mockRepo = new Mock<IPersonRepository>();
        _service = new PersonService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnNewGuid()
    {
        var dto = new PersonCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            Company = "Test Company"
        };

        _mockRepo.Setup(x => x.AddAsync(It.IsAny<Person>())).Returns(Task.CompletedTask);
        _mockRepo.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _service.CreateAsync(dto);

        result.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoData()
    {
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Person>());

        var result = await _service.GetAllAsync();

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Person?)null);

        var result = await _service.GetByIdAsync(Guid.NewGuid());

        result.Should().BeNull();
    }
}