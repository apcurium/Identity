// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.Test;
using Microsoft.Framework.DependencyInjection;

namespace Microsoft.AspNet.Identity.InMemory.Test
{
    public class InMemoryStoreTest : UserManagerTestBase<TestUser, TestRole>
    {
        protected override object CreateTestContext()
        {
            return null;
        }

        protected override void AddUserStore(IServiceCollection services, object context = null)
        {
            services.AddSingleton<IUserStore<TestUser>, InMemoryUserStore<TestUser>>();
        }

        protected override void AddRoleStore(IServiceCollection services, object context = null)
        {
            services.AddSingleton<IRoleStore<TestRole>, InMemoryRoleStore<TestRole>>();
        }

        protected override TestRole CreateTestRole(string roleName = "")
        {
            return String.IsNullOrEmpty(roleName)
                ? new TestRole(Guid.NewGuid().ToString())
                : new TestRole(roleName);
        }

        protected override void SetUserPasswordHash(TestUser user, string hashedPassword)
        {
            user.PasswordHash = hashedPassword;
        }

        protected override TestUser CreateTestUser(string namePrefix = "", string email = "", string phoneNumber = "",
            bool lockoutEnabled = false, DateTimeOffset? lockoutEnd = default(DateTimeOffset?), bool useNamePrefixAsUserName = false)
        {
            return new TestUser
            {
                UserName = useNamePrefixAsUserName ? namePrefix : string.Format("{0}{1}", namePrefix, Guid.NewGuid()),
                Email = email,
                PhoneNumber = phoneNumber,
                LockoutEnabled = lockoutEnabled,
                LockoutEnd = lockoutEnd
            };
        }

        protected override Expression<Func<TestUser, bool>> UserNamePredicate(string userName, bool contains = false)
        => contains ? (Expression<Func<TestUser, bool>>)(u => u.UserName.Contains(userName))
                    : (u => u.UserName == userName);

        protected override Expression<Func<TestRole, bool>> RoleNamePredicate(string roleName, bool contains = false)
        => contains ? (Expression<Func<TestRole, bool>>)(u => u.Name.Contains(roleName))
                    : (u => u.Name == roleName);

    }
}