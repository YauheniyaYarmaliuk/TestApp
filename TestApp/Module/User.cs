namespace TestApp.Module;

public class User(int userId, string name)
{
    public int UserId { get; } = userId;
    public string Name { get; } = name;
}