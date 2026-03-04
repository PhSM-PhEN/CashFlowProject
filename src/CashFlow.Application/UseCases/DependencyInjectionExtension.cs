using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.ToExpenses.Delete;
using CashFlow.Application.UseCases.ToExpenses.GetAll;
using CashFlow.Application.UseCases.ToExpenses.GetById;
using CashFlow.Application.UseCases.ToExpenses.Register;
using CashFlow.Application.UseCases.ToExpenses.Report.Excel;
using CashFlow.Application.UseCases.ToExpenses.Report.Pdf;
using CashFlow.Application.UseCases.ToExpenses.Update;
using CashFlow.Application.UseCases.ToLogin;
using CashFlow.Application.UseCases.ToUser.ChangePassword;
using CashFlow.Application.UseCases.ToUser.Delete;
using CashFlow.Application.UseCases.ToUser.GetUser;
using CashFlow.Application.UseCases.ToUser.Register;
using CashFlow.Application.UseCases.ToUser.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application.UseCases
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            AddUseCases(services);
            AddAutoMapper(services);
        }
        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(config => config.AddProfile<AutoMapping>());
        }
        private static void AddUseCases( IServiceCollection services)
        {
            services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
            services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
            services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
            services.AddScoped<IDeleteExpensesUseCase, DeleteExpensesUseCase>();
            services.AddScoped<IUpdateExpensesUseCase, UpdateExpensesUseCase>();
            services.AddScoped<IGenereteExpenseReportExcelUseCase, GenereteExpenseReportExcelUseCase>();
            services.AddScoped<IGenereteExpensesReportPdfUseCase, GenereteExpensesReportPdfUseCase>();
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserUseProfileCase, GetUserProfileUseCase>();
            services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
            services.AddScoped<IUpdateProfileUseCase, UpdateProfileUseCase>();
            services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        }
    }
}
