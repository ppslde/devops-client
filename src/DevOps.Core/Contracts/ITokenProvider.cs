namespace DevOps.Core.Contracts;

public interface ITokenProvider {
    Task<string?> GetToken();
}
