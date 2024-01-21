﻿using EasyFinance.Domain.Models.AccessControl;

namespace EasyFinance.Common.Tests.AccessControl
{
    public class UserBuilder : IBuilder<User>
    {
        private User user;

        public UserBuilder()
        {
            this.user = new User();
        }

        public UserBuilder AddFirstName(string firstName)
        {
            this.user.SetFirstName(firstName);
            return this;
        }

        public UserBuilder AddLastName(string lastName)
        {
            this.user.SetLastName(lastName);
            return this;
        }

        public UserBuilder AddEnabled(bool enabled)
        {
            this.user.SetEnabled(enabled);
            return this;
        }

        public User Build() => this.user;
    }
}
