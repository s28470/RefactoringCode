using System;

namespace LegacyApp;

public interface IUserValidator 
{
    public bool IsValid(string firstName, string lastName, string email, DateTime dateTime);
}