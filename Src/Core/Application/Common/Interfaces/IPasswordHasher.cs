namespace Application.Common.Interfaces
{
    /// <summary>
    /// To encrypt the password
    /// </summary>
    public interface IPasswordHasher
    {
        string Hash(string password);
        (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    }
}