namespace Domain.Messages;

public static class SuccessMessages
{
    public static string EmailSent(Guid orderId, Guid userId)
    {
        return $"The email for order with Id {orderId}, and user with id {userId} was sent successfully.";
    }
}