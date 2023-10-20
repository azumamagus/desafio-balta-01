using System.Diagnostics.CodeAnalysis;

namespace Balta.IBGE.Domain.Core;

public class Result
{
    public Result() { }
    public Result(List<string> errors) => Errors.AddRange(errors);
    public Result(string error) => Errors.Add(error);

    public List<string> Errors { get; private set; } = new();
    public bool IsFailure => !IsSuccessfully;
    public bool IsSuccessfully => !Errors.Any();

    public static Result Success() => new();
    public static Result Failure(string error) => new(error: error);
    public static Result Failure(List<string> errors) => new(errors: errors);

    public static Result<TValue> Success<TValue>(TValue value) => new(value: value);
    public static Result<TValue> Failure<TValue>(string error) => new(value: default, error: error);
    public static Result<TValue> Failure<TValue>(List<string> errors) => new(value: default, errors: errors);

    //public static Result<TValue> Create<TValue>(TValue? value) =>
    //    value is not null
    //    ? Success(value: value)
    //    : Failure<TValue>("Null value was provided");
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value)
        : base() => _value = value;
    public Result(TValue? value, string error)
        : base(error: error) => _value = value;
    public Result(TValue? value, List<string> errors)
        : base(errors: errors) => _value = value;

    [NotNull]
    public TValue Value => IsSuccessfully
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    //public static implicit operator Result<TValue>(TValue? value) => Create(value: value);
}