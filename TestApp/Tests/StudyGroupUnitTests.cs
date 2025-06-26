using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using TestApp.Enums;
using TestApp.Module;
using TestApp.Steps;

namespace TestApp.Tests;

public class StudyGroupUnitTests
{
    [Test]
    public void CreateStudyGroup_InvalidName_ThrowsException()
    {
        var users = new List<User>();

        Assert.Throws<ArgumentException>(() => { new StudyGroup(1, "Carl", Subject.Math, DateTime.UtcNow, users); });
    }

    [Test]
    public void AddUser_AddsUserToStudyGroup()
    {
        var group = new StudyGroup(1, "Math Group", Subject.Math, DateTime.UtcNow, new List<User>());
        var user = new User(1, "Jane");
        group.AddUser(user);

        group.Users.Should().Contain(user);
    }

    [Test]
    public void RemoveUser_RemovesUserFromStudyGroup()
    {
        var user = new User(1, "Carlos");
        var group = new StudyGroup(1, "Physics Group", Subject.Physics, DateTime.UtcNow, new List<User> { user });
        group.RemoveUser(user);

        group.Users.Contains(user).Should().BeFalse();
        group.Users.Count.Should().Be(0);
    }

    [Test]
    public void CreateStudyGroup_WithInvalidSubject_ThrowsException()
    {
        // Arrange
        var invalidSubjectValue = (Subject)999;
   
        // Act
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            var studyGroup = new StudyGroup(
                studyGroupId: 1,
                name: "History",
                subject: invalidSubjectValue,
                createDate: DateTime.UtcNow,
                users: new List<User>()
            );
        });

        // Assert
        StringAssert.Contains("Invalid subject", ex!.Message);
    }
}