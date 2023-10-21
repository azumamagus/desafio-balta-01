namespace Balta.IBGE.Domain.Core.Services;

public interface ISendEmailService
{
    Task SendEmailAsync(string email, CancellationToken cancellationToken);
}
