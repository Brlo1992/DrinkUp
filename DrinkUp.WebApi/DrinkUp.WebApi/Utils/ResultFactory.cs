using DrinkUp.WebApi.Model.Service;

namespace DrinkUp.WebApi.Utils {
    public static class ResultFactory {
        public static ServiceResult Create() => new ServiceResult();

        public static ServiceResult<T> CreateWithData<T>() => new ServiceResult<T>();
    }
}
