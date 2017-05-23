using System.Linq;
using System.Threading.Tasks;
using DrinkUp.WebApi.Model.Identity;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DrinkUp.WebApi.Services {
    public interface IAccountService {
        Task<ServiceResult> Register(RegisterViewModel viewModel);

        Task<ServiceResult> LogIn(LoginViewModel viewModel);

        Task<ServiceResult> LogOut();
    }

    public class AccountService : IAccountService {
        private SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<ServiceResult> Register(RegisterViewModel viewModel) {
            var result = ResultFactory.Create();
            var dbResult = await userManager.CreateAsync(GetFromViewModel(viewModel));
            if (dbResult.Succeeded) {
                result.Status = nameof(Status.Registred);
                return result;
            }
            result.AddErrors(dbResult.Errors.Select(x => $"{x.Code}: {x.Description}").ToArray());
            result.Status = nameof(Status.OperationFailed);
            return result;
        }

        public Task<ServiceResult> LogIn(LoginViewModel viewModel) {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> LogOut() {
            throw new System.NotImplementedException();
        }

        private User GetFromViewModel(RegisterViewModel viewModel) {
            return new User {
                Email = viewModel.Email,
                UserName = string.IsNullOrWhiteSpace(viewModel.UserName) ? viewModel.Email : viewModel.UserName,
                PhoneNumber = string.IsNullOrWhiteSpace(viewModel.PhoneNumber) ? string.Empty : viewModel.PhoneNumber
            };
        }
    }
}