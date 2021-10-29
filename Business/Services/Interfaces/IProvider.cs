using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IProvider<TIn, TOut> 
        where TIn : class 
        where TOut : class
    {
        public Task<TOut> Get(int key);
        public Task Update(int key, TIn dto);
        public Task Delete(int key);
        public Task<bool> KeyExists(int key);
        public Task<bool> Exists(TIn dto);
    }
}
