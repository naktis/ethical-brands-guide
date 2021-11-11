namespace Business.Security
{
    public interface IHasher
    {
        public interface IPasswordHasher
        {
            public string Hash(string password);
            public bool Check(string hash, string password);
        }
    }
}
