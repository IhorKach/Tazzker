using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazzker.Application.DTOs;
using Tazzker.Application.Interfaces;
using Tazzker.Application.Services;
using Tazzker.Domain;

namespace Tazzker.Tests
{
    public class TaskServiceTests
    {
        [Fact]
        public async Task GetTasksAsync_ReturnsTasks()
        {
            //arrange
            var userId = Guid.NewGuid();
            var mockRepo = new Mock<ITaskItemRepository>();

            var mockContext = new Mock<IUserContext>();


            mockContext.Setup(x=>x.UserId).Returns(userId);

            TaskItem? capturedTask = null;

            mockRepo.Setup(x => x.CreateTaskItemAsync(It.IsAny<TaskItem>()))
                .Callback<TaskItem>(t=>capturedTask = t)
                .Returns(Task.CompletedTask);

            var service = new TaskItemService(mockRepo.Object, mockContext.Object);

            var dto = new CreateTaskItemDto
            {
                Title = "Test Task",
                ListId = Guid.NewGuid(),
                Description = "Description"
            };

            //act
            var result = await service.CreateTaskItemAsync(dto);

            //assert 
            Assert.NotNull(capturedTask);
            Assert.Equal(dto.Title, capturedTask!.Title);
            //Assert.Equal(dto.userId, capturedTask.UserId);
            Assert.Equal(dto.ListId, capturedTask!.ListId);
            Assert.Equal("Test Task", result.Title);

        }
    }
}
