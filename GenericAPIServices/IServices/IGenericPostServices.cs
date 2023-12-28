using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.IServices
{
    public interface IGenericPostServices
    {
        Task<T> Add<T>(T obj, string endpoint) where T : class;
    }
}
