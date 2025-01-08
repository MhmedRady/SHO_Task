namespace SHO_Task.Application.Exceptions;

public sealed record ValidationError(string PropertyName, string ErrorMessage);
