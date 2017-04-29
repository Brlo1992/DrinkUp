using System.Collections.Generic;
using System.Linq;

namespace DrinkUp.WebApi.Model.Service {
    public class ServiceResult {
        public ServiceResult() {
            errors = new List<string>();
        }
        private readonly IList<string> errors;
        public bool IsValid => errors.Any() == false;
        public void AddError(string error) => errors.Add(error);

        public void AddErrors(IList<string> errors) {
            foreach (var error in errors) {
                this.errors.Add(error);
            }
        }
    }

    public class ServiceResult<T> : ServiceResult {
        public T Data { get; set; }
    }
}
