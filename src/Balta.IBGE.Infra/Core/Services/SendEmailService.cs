using Balta.IBGE.Domain.Core.Services;

namespace Balta.IBGE.Infra.Core.Services
{
    public class SendEmailService : ISendEmailService
    {
        public async Task SendEmailAsync(string email, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return;
        }
    }
}