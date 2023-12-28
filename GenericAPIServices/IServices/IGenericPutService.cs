using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.IServices
{
    public interface IGenericPutService
    {
        Task<TDTO> Update<TDTO, TId>(TDTO dto, TId id, string endpoint) where TDTO : class;
        Task<TDTO> UpdateManyToMany<TDTO, TId1, TId2>(TDTO dto, TId1 id1, TId2 id2, string endpoint) where TDTO : class;
    }
}
