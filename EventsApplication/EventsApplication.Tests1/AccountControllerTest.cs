// <copyright file="AccountControllerTest.cs">Copyright ©  2017</copyright>
using System;
using System.Web.Mvc;
using EventsApplication.Controllers;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventsApplication.Controllers.Tests
{
    /// <summary>This class contains parameterized unit tests for AccountController</summary>
    [PexClass(typeof(AccountController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClassAttribute]
    public partial class AccountControllerTest
    {
        /// <summary>Test stub for Index()</summary>
        [PexMethod]
        public ActionResult IndexTest([PexAssumeUnderTest]AccountController target)
        {
            ActionResult result = target.Index();
            return result;
            // TODO: add assertions to method AccountControllerTest.IndexTest(AccountController)
        }
    }
}
