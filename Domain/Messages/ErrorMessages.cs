namespace Domain.Messages;

public static class ErrorMessages
{
    public static string UserNotFound(Guid id) => $"The user with Id {id} was not found.";
    public static string OrderException(Guid id, string ex) => $"the  order with Id {id}, generated an exception: {ex}";
}