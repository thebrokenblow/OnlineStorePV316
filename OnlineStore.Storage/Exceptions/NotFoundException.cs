namespace OnlineStore.Storage.Exceptions;

public class NotFoundException(string message) : Exception(message)
{ 
}