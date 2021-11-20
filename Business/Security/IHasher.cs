namespace Business.Security
{
    public interface IHasher
    {
        public string Hash(string password);
        public bool Check(string hash, string password);
    }
}
