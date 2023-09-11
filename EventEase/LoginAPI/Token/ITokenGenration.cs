namespace LoginAPI.Token
{
    public interface ITokenGenration
    {
        public string GenrateToken(string email);
    }
}
