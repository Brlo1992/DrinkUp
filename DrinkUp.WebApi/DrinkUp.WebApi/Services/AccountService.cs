using DrinkUp.WebApi.Model.Identity;
using DrinkUp.WebApi.Model.Service;
using DrinkUp.WebApi.Utils;
using DrinkUp.WebApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkUp.WebApi.Services {
    public interface IAccountService {
        Task<ServiceResult> Register(RegisterViewModel viewModel);

        Task<ServiceResult> LogIn(LoginViewModel viewModel);

        Task<ServiceResult> LogOut();
    }

    public class AccountService : IAccountService {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<ServiceResult> Register(RegisterViewModel viewModel) {
            var result = ResultFactory.Create();
            var user = GetFromViewModel(viewModel);
            try {
                var dbResult = await userManager.CreateAsync(user, viewModel.Password);

                if (dbResult.Succeeded) {
                    await signInManager.SignInAsync(user, false);
                    result.Status = nameof(Status.Registred);
                    return result;
                }

                result.AddErrors(dbResult.Errors.Select(x => $"{x.Code}: {x.Description}").ToArray());
                result.Status = nameof(Status.OperationFailed);
                return result;
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public async Task<ServiceResult> LogIn(LoginViewModel viewModel) {
            var result = ResultFactory.Create();

            try {
                var dbResult =
                    await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe,
                        false);

                if (dbResult.Succeeded) {
                    result.Status = nameof(Status.LoggedIn);
                    return result;
                }
                result.AddError(ErrorHelper.LoginFailed);
                result.Status = nameof(Status.OperationFailed);
                return result;
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
        }

        public async Task<ServiceResult> LogOut() {
            var result = ResultFactory.Create();

            try {
                await signInManager.SignOutAsync();

                result.Status = nameof(Status.LoggedOut);
                return result;
            }
            catch (Exception ex) {
                result.AddError(ex.Message);
                result.Status = nameof(Status.OperationFailed);
            }
            return result;
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