using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUp.WebApi.Model.Service
{
    public class ServiceResult
    {
        public bool IsValid { get; private set; }

    }

    public class ServiceResult<T> {
        public T Data { get; set; }
    }
}
