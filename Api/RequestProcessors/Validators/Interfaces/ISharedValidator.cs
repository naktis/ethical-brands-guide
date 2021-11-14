namespace Api.RequestProcessors.Validators.Interfaces
{
    public interface ISharedValidator
    {
        public bool TextGoodLength(string text, int max, int min = 0);
    }
}
