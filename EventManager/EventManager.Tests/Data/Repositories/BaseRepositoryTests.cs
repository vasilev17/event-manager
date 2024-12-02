using EventManager.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

// Mock entity for testing
public class TestEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

// Mock repository inheriting from BaseRepository
public class TestRepository : BaseRepository<TestEntity>
{
    public TestRepository(DbContext context) : base(context) { }
}

public class BaseRepositoryTests
{
    private readonly Mock<DbSet<TestEntity>> _mockSet;
    private readonly Mock<DbContext> _mockContext;
    private readonly TestRepository _repository;

    public BaseRepositoryTests()
    {
        _mockSet = new Mock<DbSet<TestEntity>>();
        _mockContext = new Mock<DbContext>();

        // Set up the mocked DbSet
        var data = new List<TestEntity>().AsQueryable();

        _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(data.Provider);
        _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(data.Expression);
        _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
        _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);

        // Initialize repository with mocked DbContext
        _repository = new TestRepository(_mockContext.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldAddEntity()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test Entity" };

        // Act
        var result = await _repository.AddAsync(entity);

        // Assert
        _mockSet.Verify(m => m.AddAsync(entity, default), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldCallSaveChanges()
    {
        // Act
        var result = await _repository.SaveChangesAsync();

        // Assert
        _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowNotImplementedException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _repository.DeleteAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task EditAsync_ShouldThrowNotImplementedException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _repository.EditAsync(Guid.NewGuid(), new TestEntity()));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotImplementedException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => _repository.GetByIdAsync(Guid.NewGuid()));
    }
}
