namespace UserService.Application.Features.Email;

public static class EmailTemplates
{
    public static string RecoverEmailBody(string link) =>
        $"""
         <h1>Recover account</h1>
         <p>Go thought this link to recover:</p>
         <a href="{link}">Reset!</a>
         <p>This link active only 1 hour.</p>
         """;

    public static string ResetPasswordEmailBody(string link) =>
        $"""
         <h1>Reset password</h1>
         <p>Go thought this link to reset:</p>
         <a href="{link}">Reset!</a>
         <p>This link active only 24 hours.</p>
         """;

    public static string ConfirmEmailBody(string link) =>
        $"""
         <h1>Email confirmation</h1>
         <p>Go thought this link to confirm:</p>
         <a href="{link}">Confirm!</a>
         <p>This link active only 24 hours.</p>
         """;
}