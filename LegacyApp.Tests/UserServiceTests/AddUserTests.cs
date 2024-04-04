namespace LegacyApp.Tests.UserServiceTests;

public class AddUserTests
{
    [Fact]
    public void AddUser_ValidInput_Returns_True()
    {
        const string firstName = "John";
        const string lastName = "Doe";
        const string email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1990, 1, 1);
        const int clientId = 6;

        var result = Act(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }

    [Fact]
    public void AddUser_InvalidInput_Returns_False()
    {
        const string firstName = "";
        const string lastName = "Kowalski";
        const string email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1999, 1, 1);
        const int clientId = 1;
        
        var result = Act(firstName, lastName, email, dateOfBirth, clientId);
        
        Assert.False(result);
    }

    [Fact]
    public void AddUser_ClientType_VeryImportantClient_Returns_True()
    {
        const string firstName = "John";
        const string lastName = "Malewski";
        const string email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1999, 1, 1);
        const int clientId = 2; // VeryImportantClient

        var result = Act(firstName, lastName, email, dateOfBirth, clientId);
        
        Assert.True(result);
    }

    [Fact]
    public void AddUser_ClientType_ImportantClient_Returns_True()
    {
        const string firstName = "John";
        const string lastName = "Smith";
        const string email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1999, 1, 1);
        const int clientId = 3; // ImportantClient

        var result = Act(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }

    [Fact]
    public void AddUser_ClientType_NormalClient_LowCreditLimit_Returns_False()
    {
        const string firstName = "John";
        const string lastName = "Kowalski";
        const string email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1999, 1, 1);
        const int clientId = 1; // NormalClient with low Credit limit
        
        var result = Act(firstName, lastName, email, dateOfBirth, clientId);
        
        Assert.False(result);
    }

    [Fact]
    public void AddUser_ClientType_NormalClient_HighCreditLimit_Returns_True()
    {
        const string firstName = "John";
        const string lastName = "Kwiatkowski";
        const string email = "john.doe@example.com";
        var dateOfBirth = new DateTime(1999, 1, 1);
        const int clientId = 5; // NormalClient with high Credit limit
        
        var result = Act(firstName, lastName, email, dateOfBirth, clientId);

        Assert.True(result);
    }

    private static bool Act(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        var service = new UserService();
        var result = service.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        return result;
    }
}