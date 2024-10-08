﻿using System.Threading.Tasks;
using EasyFinance.Domain.Models.AccessControl;
using EasyFinance.Domain.Models.Financial;
using EasyFinance.Domain.Models.FinancialProject;

namespace EasyFinance.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IGenericRepository<Project> ProjectRepository { get; }
        IGenericRepository<UserProject> UserProjectRepository { get; }
        IGenericRepository<Income> IncomeRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Expense> ExpenseRepository { get; }
        IGenericRepository<ExpenseItem> ExpenseItemRepository { get; }

        Task CommitAsync();
    }
}
