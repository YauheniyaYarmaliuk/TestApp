using TestApp.Enums;

namespace TestApp.Module;

public class StudyGroup
{
    public int StudyGroupId { get; }
    public string Name { get; }
    public Subject Subject { get; }
    public DateTime CreateDate { get; }
    public List<User> Users { get; private set; }

    public StudyGroup(int studyGroupId, string name, Subject subject, DateTime createDate, List<User> users)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 5 || name.Length > 30)
            throw new ArgumentException("Name must be between 5 and 30 characters.");

        if (!Enum.IsDefined(typeof(Subject), subject))
            throw new ArgumentException("Invalid subject.");

        StudyGroupId = studyGroupId;
        Name = name;
        Subject = subject;
        CreateDate = createDate;
        Users = users ?? new List<User>();
    }

    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (!Users.Exists(u => u.UserId == user.UserId))
        {
            Users.Add(user);
        }
    }

    public void RemoveUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        Users.RemoveAll(u => u.UserId == user.UserId);
    }
}