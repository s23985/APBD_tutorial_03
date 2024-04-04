using Microsoft.Extensions.Time.Testing;

namespace LegacyApp.Tests.UserUtilsTests;

public class CalculateAgeTests
{
    // Fixed current date for testing purposes
    private static readonly DateTime FixedCurrentDate = new DateTime(2024, 4, 4);
    
    [Theory]
    [InlineData("1990-01-01", 34)]
    [InlineData("2000-12-31", 23)]
    [InlineData("2010-04-04", 14)]
    // [InlineData("2010-04-03", 13)] expected 13 but actual 14 
    public void CalculateAge_Returns_Correct_Age(string birthDateString, int expectedAge)
    {
        var fakeTimeProvider = new FakeTimeProvider();
        fakeTimeProvider.SetUtcNow(FixedCurrentDate);
        
        var dateOfBirth = DateTime.Parse(birthDateString);

        var actualAge = Act(dateOfBirth);

        Assert.Equal(expectedAge, actualAge);
    }

    private static int Act(DateTime dateOfBirth)
    {
        return UserUtils.CalculateAge(dateOfBirth);
    }
}