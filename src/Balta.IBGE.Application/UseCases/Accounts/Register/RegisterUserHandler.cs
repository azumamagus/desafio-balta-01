using Balta.IBGE.Domain.Accounts.Entities;
using Balta.IBGE.Domain.Accounts.Repositories;
using Balta.IBGE.Domain.Accounts.ValueObjects;
using Balta.IBGE.Domain.Core;
using Balta.IBGE.Domain.Core.Services;

using MediatR;

namespace Balta.IBGE.Application.UseCases.Accounts.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ISendEmailService _sendEmailService;

    public RegisterUserHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ISendEmailService sendEmailService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _sendEmailService = sendEmailService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new Exception("Forced Exception");

        var email = new Email(request.Email);
        var password = new Password(request.Password);
        var user = new User(email, password);

        var exists = await _userRepository.AnyAsync(request.Email, cancellationToken);
        if (exists)
            return Result.Failure<Guid>("Email already exists");

        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            await _sendEmailService.SendEmailAsync(user.Email, cancellationToken);
        }
        catch
        {
            // Do nothing
        }

        return Result.Success<Guid>(user.Id);
    }
}
