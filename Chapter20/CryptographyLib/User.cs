namespace Packt.Shared;
public class User
{
    public string Name { get; set; }
    public string Salt { get; set; }
    public string SaltedHashedPassword { get; set; }
    public User(string name, string salt,
    string saltedHashedPassword)
    {
        Name = name;
        Salt = salt;
        SaltedHashedPassword = saltedHashedPassword;
    }
}
