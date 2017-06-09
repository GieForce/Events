// <copyright file="AccountRepositoryTest.cs">Copyright ©  2017</copyright>
using System;
using EventsApplication.App_DAL;
using EventsApplication.Controllers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventsApplication.Controllers.Tests
{
    /// <summary>This class contains parameterized unit tests for AccountRepository</summary>
    [PexClass(typeof(AccountRepository))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class AccountRepositoryTest
    {
        /// <summary>Test stub for .ctor(IAccountContext)</summary>
        [PexMethod]
        public AccountRepository ConstructorTest(IAccountContext context)
        {
            AccountRepository target = new AccountRepository(context);
            target.
            return target;
            
            // TODO: add assertions to method AccountRepositoryTest.ConstructorTest(IAccountContext)
        }
    }
}
