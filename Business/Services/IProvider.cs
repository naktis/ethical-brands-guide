using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IProvider<TIn, TOut> 
        where TIn : class 
        where TOut : class
    {
        public Task<TOut> Get(int key);
        public Task Update(int key, TIn dto);
        public Task Delete(int key);
        public Task<bool> KeyExists(int key);

    }
}
