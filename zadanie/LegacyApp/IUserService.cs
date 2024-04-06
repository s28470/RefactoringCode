using System;

namespace LegacyApp;

public interface IUserService
{
    bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId);
    
}