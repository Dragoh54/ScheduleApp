namespace UserService.Application.Handlers.Email;

public record EmailSettings
{
    public string SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; } = 465;
    public string SmtpUsername { get; set; } = string.Empty;     
    public string SmtpPassword { get; set; } = string.Empty;
    public string FromAddress { get; set; } = string.Empty;
    public string FromName { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = false;
}