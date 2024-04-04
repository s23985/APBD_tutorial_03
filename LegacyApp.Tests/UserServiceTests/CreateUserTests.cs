namespace LegacyApp.Tests.UserServiceTests;

public class CreateUserTests
{
    [Fact]
    public void CreateUser_Returns_User_With_Correct_Properties()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var client = new Client
        {
            Id = 1,
            Name = "TestClient",
            Email = "client@example.com",
            Address = "123 Test St",
            Type = ClientType.NormalClient
        };

        var user = Act(firstName, lastName, email, dateOfBirth, client);

        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(email, user.EmailAddress);
        Assert.Equal(dateOfBirth, user.DateOfBirth);
        Assert.Equal(client.Id, user.Client.Id);
    }

    private static User Act(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
    {
        var service = new UserService();
        var user = service.CreateUser(firstName, lastName, email, dateOfBirth, client);

        return user;
    }
}