using System;

namespace LegacyApp;

public class UserValidator : IUserValidator
{
    public bool IsValid(string firstName, string lastName, string email, DateTime dateTime)
    {
        return IsNameValid(firstName, lastName) && IsEmailValid(email) && IsAgeValid(dateTime);
    }


    private bool IsNameValid(string firstName, string lastName)
    {
        return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
    }

    private bool IsEmailValid(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    private bool IsAgeValid(DateTime dateTime)
    {
        var now = DateTime.Now;
        int age = now.Year - dateTime.Year;
        if (now.Month < dateTime.Month || (now.Month == dateTime.Month && now.Day < dateTime.Day))
            age--;
        return age >= 21;
    }
}