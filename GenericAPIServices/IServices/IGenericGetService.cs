using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.IServices
{
    public interface IGenericGetService
    {
        Task<T> Get<T>(string endpoint, params object[] ids) where T : class;
        Task<IEnumerable<T>> GetAll<T>(string endpoint) where T : class;
        Task<IEnumerable<T>> GetAllByIds<T>(string endpoint, params object[] ids) where T : class;
        Task<int> GetCount<T>(string endpoint, Expression<Func<T, bool>> filter) where T : class;
    }
}
