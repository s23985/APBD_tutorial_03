using Microsoft.Extensions.Time.Testing;

namespace LegacyApp.Tests.UserServiceTests;

public class ValidateInputTests
{
    // Fixed current date for testing purposes
    private static readonly DateTime FixedCurrentDate = new DateTime(2024, 4, 4);
    
    [Theory]
    [InlineData("John", "Doe", "john.doe@example.com", "1990-01-01", true)]
    [InlineData("", "Doe", "john.doe@example.com", "1990-01-01", false)]
    [InlineData("John", "", "john.doe@example.com", "1990-01-01", false)]
    [InlineData("John", "Doe", "invalid-email", "1990-01-01", false)]
    [InlineData("John", "Doe", "john.doe@example.com", "2003-04-10", false)]
    public void ValidateInput_Returns_Correct_Result(string firstName, string lastName, string email, string dateOfBirthString, bool expectedResult)
    {
        var fakeTimeProvider = new FakeTimeProvider();
        fakeTimeProvider.SetUtcNow(FixedCurrentDate);
        
        var dateOfBirth = DateTime.Parse(dateOfBirthString);

        var result = Act(firstName, lastName, email, dateOfBirth);

        Assert.Equal(expectedResult, result);
    }

    private static bool Act(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        var service = new UserService();
        var result = service.ValidateInput(firstName, lastName, email, dateOfBirth);

        return result;
    }
}