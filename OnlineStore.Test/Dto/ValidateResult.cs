namespace OnlineStore.Test.Dto;

public class ValidateResult
{
    public required bool IsValid { get; init; }
    public required string Error { get; init; }
}