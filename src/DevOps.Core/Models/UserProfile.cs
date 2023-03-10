namespace DevOps.Core.Models;

public class UserProfile {
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string PublicAlias { get; set; }
    public string EmailAddress { get; set; }
}
