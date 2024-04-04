using System;

namespace LegacyApp;

public class UserService
{
    private readonly ClientRepository _clientRepository;
    private readonly UserCreditService _creditService;

    public UserService()
    {
        _clientRepository = new ClientRepository();
        _creditService = new UserCreditService();
    }
    
    public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
    {
        if (ValidateInput(firstName, lastName, email, dateOfBirth) == false)
            return false;

        var client = _clientRepository.GetById(clientId);

        var user = CreateUser(firstName, lastName, email, dateOfBirth, client);

        var creditLimit = _creditService.GetCreditLimit(user.LastName);

        SetUserCredit(client, user, creditLimit);

        if (IsCreditTooLow(user) == false)
            return false;

        UserDataAccess.AddUser(user);
        return true;
    }

    private void SetUserCredit(Client client, User user, int creditLimit)
    {
        switch (client.Type)
        {
            case ClientType.VeryImportantClient:
                user.HasCreditLimit = false;
                break;
            case ClientType.ImportantClient:
            {
                creditLimit *= 2;
                SetCreditLimit(user, creditLimit);
                break;
            }
            case ClientType.NormalClient:
            default:
            {
                user.HasCreditLimit = true;
                SetCreditLimit(user, creditLimit);
                break;
            }
        }
    }

    private void SetCreditLimit(User user, int creditLimit)
    {
        using (_creditService)
        {
            user.CreditLimit = creditLimit;
        }
    }

    private static bool IsCreditTooLow(User user)
    {
        if (user.HasCreditLimit && user.CreditLimit < 500)
        {
            return false;
        }

        return true;
    }

    public bool ValidateInput(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            return false;

        if (email.Contains('@') == false || email.Contains('.') == false)
            return false;

        var age = UserUtils.CalculateAge(dateOfBirth);
        return age >= 21;
    }
    
    public User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
    {
        var user = new User
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            FirstName = firstName,
            LastName = lastName
        };

        return user;
    }
}