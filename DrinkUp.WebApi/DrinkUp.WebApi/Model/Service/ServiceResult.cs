using DrinkUp.WebApi.Utils;
using System.Collections.Generic;
using System.Linq;

namespace DrinkUp.WebApi.Model.Service {
    public class ServiceResult {
        public ServiceResult() {
            Errors = SimpleFactory<List<string>>.Create();
        }
        
        public string Status { get; set; }

        public bool IsValid => Errors.Any() == false;

        public IList<string> Errors { get; }

        public void AddError(string error) => Errors.Add(error);

        public void AddErrors(IList<string> errors) {
            if (!errors.Any()) return;
            foreach (var error in errors) {
                Errors.Add(error);
            }
        }
    }

    public class ServiceResult<T> : ServiceResult {
        public T Data { get; set; }
    }
}
