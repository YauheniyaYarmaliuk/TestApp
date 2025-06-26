using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using TestApp.Enums;
using TestApp.Interfaces;
using TestApp.Module;
using TestApp.Steps;

namespace TestApp.Tests;

public class StudyGroupComponentTests
{
    private Mock<IStudyGroupRepository> _repo = null!;
    private StudyGroupComponent _component = null!;

    [SetUp]
    public void SetUp()
    {
        _repo = new Mock<IStudyGroupRepository>();
        _component = new StudyGroupComponent(_repo.Object);
    }

    [Test]
    public async Task CreateStudyGroup_Valid_ReturnsOk()
    {
        // Arrange
        var group = new StudyGroup(1, "Chemistry Group", Subject.Chemistry, DateTime.UtcNow, new List<User>());

        // Act
        _repo.Setup(r => r.CreateStudyGroup(group)).Returns(Task.CompletedTask);
        var result = await _component.CreateStudyGroup(group);

        // Assert
        result.Should().BeOfType<OkResult>();
    }

    [Test]
    public async Task JoinStudyGroup_CallsRepository()
    {
        // Arrange
        int groupId = 1, userId = 123;

        // Act
        var result = await _component.JoinStudyGroup(groupId, userId);

        // Assert
        _repo.Verify(r => r.JoinStudyGroup(groupId, userId), Times.Once);
        result.Should().BeOfType<OkResult>();
    }

    [Test]
    public async Task SearchStudyGroups_ReturnsSubjectFiltered()
    {
        // Arrange
        _repo.Setup(r => r.SearchStudyGroups("Math"))
            .ReturnsAsync(new List<StudyGroup>
                { new(1, "Math Group", Subject.Math, DateTime.UtcNow, new List<User>()) });

        // Act
        var result = await _component.SearchStudyGroups("Math") as OkObjectResult;
        var list = result!.Value as List<StudyGroup>;

        // Assert
        list.Should().NotBeNull();
        list.Count.Should().Be(1);
        list[0].Subject.Should().Be(Subject.Math);
    }

    [Test]
    public async Task LeaveStudyGroup_CallsRepositoryAndReturnsOk()
    {
        // Arrange
        var studyGroupId = 1;
        var userId = 123;

        var mockRepo = new Mock<IStudyGroupRepository>();
        mockRepo
            .Setup(r => r.LeaveStudyGroup(studyGroupId, userId))
            .Returns(Task.CompletedTask)
            .Verifiable();
        var controller = new StudyGroupComponent(mockRepo.Object);

        // Act
        var result = await controller.LeaveStudyGroup(studyGroupId, userId);

        // Assert
        mockRepo.Verify(r => r.LeaveStudyGroup(studyGroupId, userId), Times.Once);
        result.Should().BeOfType<OkResult>();
    }

    [Test]
    public async Task GetStudyGroups_ReturnsListOfStudyGroups()
    {
        // Arrange
        var mockRepo = new Mock<IStudyGroupRepository>();
        var expectedGroups = new List<StudyGroup>
        {
            new(1, "Math Group", Subject.Math, DateTime.UtcNow, new List<User>()),
            new(2, "Chemistry Group", Subject.Chemistry, DateTime.UtcNow, new List<User>())
        };

        mockRepo.Setup(repo => repo.GetStudyGroups()).ReturnsAsync(expectedGroups);

        var controller = new StudyGroupComponent(mockRepo.Object);

        // Act
        var result = await controller.GetStudyGroups();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.Value.Should().Be(expectedGroups);
        mockRepo.Verify(repo => repo.GetStudyGroups(), Times.Once);
    }

    [Test]
    public async Task CreateStudyGroup_DuplicateSubject_ReturnsConflict()
    {
        // Arrange
        var expectedErrorMessage = "StudyGroup for subject already exists";
        var mockRepo = new Mock<IStudyGroupRepository>();
        mockRepo.Setup(r => r.CreateStudyGroup(It.IsAny<StudyGroup>()))
            .ThrowsAsync(new InvalidOperationException(expectedErrorMessage));
        var controller = new StudyGroupComponent(mockRepo.Object);
        var newGroup = new StudyGroup(2, "ChemTeam", Subject.Chemistry, DateTime.UtcNow, new List<User>());

        // Act
        var result = await controller.CreateStudyGroup(newGroup);

        // Assert
        result.Should().BeOfType<ConflictObjectResult>();
        var conflict = result as ConflictObjectResult;
        conflict?.Value.Should().Be(expectedErrorMessage);
    }
}