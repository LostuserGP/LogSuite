using Radzen;

namespace LogSuite.Client.Services;

public class Notify
{
    private readonly NotificationService _notificationService;

    public Notify(NotificationService notService)
    {
        _notificationService = notService;
    }
    
    public void Success(string title, string message)
    {
        _notificationService.Notify(new NotificationMessage
        {
            Severity = NotificationSeverity.Success,
            Summary = title,
            Detail = message,
            Duration = 3000
        });
    }
    
    public void Error(string title, string message)
    {
        _notificationService.Notify(new NotificationMessage
        {
            Severity = NotificationSeverity.Error,
            Summary = title,
            Detail = message,
            Duration = 3000
        });
    }
}