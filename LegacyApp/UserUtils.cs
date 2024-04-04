using System;

namespace LegacyApp;

public class UserUtils
{
    public static int CalculateAge(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        var age = now.Year - dateOfBirth.Year;
        
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            age--;

        return age;
    }
}