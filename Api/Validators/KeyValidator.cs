using Api.Validators.Interfaces;

namespace Api.Validators
{
    public class KeyValidator : IKeyValidator
    {
        public bool Validate(int key)
        {
            return key >= 0;
        }
    }
}
