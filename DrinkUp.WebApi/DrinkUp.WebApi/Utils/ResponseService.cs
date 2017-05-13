using DrinkUp.WebApi.Model.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DrinkUp.WebApi.Utils {
    public interface IResponseService {
        ObjectResult GetResponse(ServiceResult serviceResult);

        ObjectResult GetResponse<T>(ServiceResult<T> serviceResult);
    }
    public class ResponseService : IResponseService{
        public ObjectResult GetResponse(ServiceResult serviceResult) {
            return new OkObjectResult(serviceResult) ;
        }

        public ObjectResult GetResponse<T>(ServiceResult<T> serviceResult) {
            return new OkObjectResult(serviceResult);
        }
    }
}
