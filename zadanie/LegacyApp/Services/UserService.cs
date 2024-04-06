using System;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IUserValidator _userValidator = new UserValidator();
        private readonly IClientRepository _clientRepository = new ClientRepository();
        private readonly IUserCreditService _userCreditService = new UserCreditService();

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (_userValidator.IsValid(firstName, lastName, email, dateOfBirth))
            {
                var client = _clientRepository.GetById(clientId);
                var user = CreateUser(firstName, lastName, email, dateOfBirth, client);
                SetCreditLimit(client,user);
                if (user.HasCreditLimit && user.CreditLimit < 500)
                {
                    return false;
                }
                
                return true;
            }
            
            return false;
        }
        
        private void SetCreditLimit(Client client,User user)
        {
            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    user.CreditLimit = _userCreditService.GetCreditLimit(user.LastName) * 2;
                    break;
                default:
                    user.HasCreditLimit = true;
                    user.CreditLimit = _userCreditService.GetCreditLimit(user.LastName);
                    break;
            }
            
        }
        
        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            return new User()
            {
                Client = client,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };
        }
        
    }
}
