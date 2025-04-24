using Moq;
using Xunit;
using PhoneBook.Application.DTOs;
using PhoneBook.Application.Services;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Interfaces;

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
    public async Task CreateAsync_ShouldReturnNewPersonId()
    {
        var dto = new PersonCreateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Company = "Test Co"
        };

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Person>())).Returns(Task.CompletedTask);
        _mockRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var result = await _service.CreateAsync(dto);

        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoPersons()
    {
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Person>());

        var result = await _service.GetAllAsync();

        Assert.Empty(result);
    }
}