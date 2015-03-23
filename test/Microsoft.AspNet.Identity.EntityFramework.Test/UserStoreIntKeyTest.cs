// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;
using Xunit;

namespace Microsoft.AspNet.Identity.EntityFramework.Test
{
    public class IntUser : IdentityUser<int>
    {
        public IntUser()
        {
            UserName = Guid.NewGuid().ToString();
        }
    }

    public class IntRole : IdentityRole<int>
    {
        public IntRole()
        {
            Name = Guid.NewGuid().ToString();
        }
    }

    [TestCaseOrderer("Microsoft.AspNet.Identity.Test.PriorityOrderer", "Microsoft.AspNet.Identity.EntityFramework.Test")]
    public class UserStoreIntTest : SqlStoreTestBase<IntUser, IntRole, int>
    {
        private readonly string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=SqlUserStoreIntTest" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ";Trusted_Connection=True;";

        public override string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        protected override Expression<Func<IntRole, bool>> RoleNamePredicate(string roleName, bool contains = false)
        => contains ? (Expression<Func<IntRole, bool>>)(u => u.Name.Contains(roleName))
                    : (u => u.Name == roleName);

        protected override Expression<Func<IntUser, bool>> UserNamePredicate(string userName, bool contains = false)
        => contains ? (Expression<Func<IntUser, bool>>)(u => u.UserName.Contains(userName))
                    : (u => u.UserName == userName);
    }
}