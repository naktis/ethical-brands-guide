using Api.RequestProcessors.Validators.Interfaces;

namespace Api.RequestProcessors.Validators
{
    public class SharedValidator : ISharedValidator
    {
        public bool TextGoodLength(string text, int max, int min = 0)
        {
            return text.Length >= min && text.Length <= max;
        }
    }
}
