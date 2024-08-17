namespace LinkTrim.Api.Core.Interfaces;

public interface IUrlHashingService
{
    string GetHash(string url);
}