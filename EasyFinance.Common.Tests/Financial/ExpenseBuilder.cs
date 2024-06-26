﻿using EasyFinance.Domain.Models.Financial;

namespace EasyFinance.Common.Tests.Financial
{
    public class ExpenseBuilder : BaseExpenseBuilder<Expense>
    {
        public ExpenseBuilder() : base(new Expense())
        {
        }

        public ExpenseBuilder AddGoal(int goal)
        {
            this.entity.SetGoal(goal);
            return this;
        }
    }
}
