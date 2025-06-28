namespace OnlineStore.Test.Dto;

public class ErrorResponse
{
    public required int StatusCode { get; init; }
    public required string Message { get; init; }
}