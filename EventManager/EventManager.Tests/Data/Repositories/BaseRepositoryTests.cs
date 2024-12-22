using EventManager.Data;
using EventManager.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BaseRepositoryTests
{
    // Mock entity for testing
    public class TestEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    // Mock repository inheriting from BaseRepository
    public class TestRepository : BaseRepository<TestEntity>
    {
        public TestRepository(ApplicationDbContext context) : base(context) { }
    }

    public class BaseRepositoryTests
    {
        private readonly Mock<DbSet<TestEntity>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly TestRepository _repository;

        public BaseRepositoryTests()
        {
            _mockSet = new Mock<DbSet<TestEntity>>();
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            // Set up the mocked DbSet
            var data = new List<TestEntity>().AsQueryable();

            _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            // Initialize repository with mocked ApplicationDbContext
            _repository = new TestRepository(_mockContext.Object);
        }

        // ORIGINAL: Tests that AddAsync adds the entity and saves changes successfully.
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

        // ORIGINAL: Tests that SaveChangesAsync calls SaveChanges on the DbContext and returns true.
        [Fact]
        public async Task SaveChangesAsync_ShouldCallSaveChanges()
        {
            // Act
            var result = await _repository.SaveChangesAsync();

            // Assert
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            Assert.True(result);
        }

        // UPDATED: Tests that SaveChangesAsync returns false when no changes are saved to the DbContext.
        [Fact]
        public async Task SaveChangesAsync_ShouldReturnFalse_WhenNoChanges()
        {
            // Arrange
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(0);

            // Act
            var result = await _repository.SaveChangesAsync();

            // Assert
            Assert.False(result);
        }

        // UPDATED: Tests that SaveChangesAsync throws an exception when SaveChangesAsync fails.
        [Fact]
        public async Task SaveChangesAsync_ShouldThrowException_WhenSaveFails()
        {
            // Arrange
            _mockContext.Setup(m => m.SaveChangesAsync(default))
                        .Throws(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _repository.SaveChangesAsync());
        }

        // UPDATED: Simulates adding duplicate entities and ensures appropriate exception handling.
        [Fact]
        public async Task AddAsync_ShouldHandleDuplicateEntity()
        {
            // Arrange
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Duplicate Entity" };

            _mockSet.Setup(m => m.AddAsync(entity, default))
                    .Throws(new DbUpdateException("Duplicate entity"));

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => _repository.AddAsync(entity));
        }
    }
}

