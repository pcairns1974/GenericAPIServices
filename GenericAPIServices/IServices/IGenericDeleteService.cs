using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.IServices
{
    public interface IGenericDeleteService
    {
        Task<HttpResponseMessage> Delete<TId>(TId id, string endpoint);
        Task<HttpResponseMessage> DeleteManyToManyRecord<TId1, TId2>(TId1 parentId, TId2 childId, string endpoint);
    }
}
