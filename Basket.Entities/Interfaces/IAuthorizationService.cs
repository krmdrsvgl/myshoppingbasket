namespace ShoppingBasket.Core.Interfaces
{
    public interface ICustomAuthorizationService
    {
        bool IsConsumerAuthorized(string secretKey, string userId);
    }
}