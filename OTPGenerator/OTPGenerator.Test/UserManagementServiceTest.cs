using NUnit.Framework;
using OTPGenerator.Service.CustomException;
using OTPGenerator.Service.Model;
using OTPGenerator.Service.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Test
{
    [TestFixture]
    public class UserManagementServiceTest
    {
        private IUserManagementService userManagementService;

        [SetUp]
        public void SetUp()
        {
            this.userManagementService = new UserManagementService();
        }

        //Method - UserManagementService - GetPasswordForUser
        //verify if the password is valid for one user only
        //Concurrently generate password and test each password is unique by user
        //For concurrent execution, used Parallel.ForEach 
        [Test]
        public void GeneratedPasswordIsUniqueForOneUserOnlyTest()
        {
            var user_1 = new User() { UserId = "TestId1" };
            var user_2 = new User() { UserId = "TestId2" };
            var user_3 = new User() { UserId = "TestId3" };

            var userList = new List<User>() { user_1, user_2, user_3 };

            Parallel.ForEach(userList, (currentFile) =>
            {
                userManagementService.GetPasswordForUser(currentFile);
            });

            Assert.That(userList.GroupBy(n => n.OTPassword).Any(c => c.Count() > 1), Is.EqualTo(false));
        }

        //Method - UserManagementService - GetPasswordForUser
        //Verify GetPasswordForUser returns correct data
        //Ensure all user details are filled and expected results
        [Test]
        public void GetPasswordForUserMethodReturnsCorrectValueTest()
        {
            var user = new User() { UserId = "TestId1" };

            userManagementService.GetPasswordForUser(user);

            Assert.That(user.UserId, Is.EqualTo("TestId1"));
            Assert.That(String.IsNullOrEmpty(user.OTPassword), Is.False);
            Assert.That(user.OTPassword.Length, Is.EqualTo(6));
            Assert.That(user.OTPCreatedDateTime.HasValue, Is.True);
            Assert.That(user.IsOTPasswordValid, Is.True);
        }


        //Method - UserManagementService - GetPasswordForUser
        //Test for Null or Empty UserId throws ExceptionCode USERID_NULL_OR_EMPTY 100
        [Test]
        public void GetPasswordForUserThrowsExceptionTest()
        {
            try
            {
                var user = new User() { UserId = "" };
                userManagementService.GetPasswordForUser(user);
            }
            catch (Exception e)
            {
                int? c = null;
                if (e is CustomCodeException) c = (e as CustomCodeException).code;
                Assert.That(c, Is.EqualTo(100));
            }
        }

        //Method - UserManagementService - ValidatePasswordForUser
        //Test for Null or Empty UserId and OTPassword throws ExceptionCode INVALID_USER_DETAILS 101
        [Test]
        public void ValidatePasswordForUserThrowsExceptionTest()
        {
            try
            {
                var user = new User() { UserId = "TestId1", OTPassword = null };
                userManagementService.ValidatePasswordForUser(user);
            }
            catch (Exception e)
            { // user Assert.Throws (http://www.nunit.org/index.php?p=exceptionAsserts&r=2.5.10)
                int? c = null;
                if (e is CustomCodeException) c = (e as CustomCodeException).code;
                Assert.That(c, Is.EqualTo(101));
            }
        }

        //Method - UserManagementService - ValidatePasswordForUser
        //Verify Genereated Password is same each time per user with in 30 second
        //Used System.Threading.Thread.Sleep to hang the process

        [Test]
        public void ValidatePasswordForUserLessThan30SecondsTest()
        {
            var user = new User() { UserId = "TestId1" };

            userManagementService.GetPasswordForUser(user);
           
            //hang the process for 2 second
            System.Threading.Thread.Sleep(2000);

            userManagementService.ValidatePasswordForUser(user);

            Assert.That(user.IsOTPasswordValid, Is.True);

        }

        //Method - UserManagementService - ValidatePasswordForUser
        //Verify Genereated Password is different per user more than 30 second
        //Used System.Threading.Thread.Sleep to hang the process more than 30 second and expected result is false

        [Test]
        public void ValidatePasswordForUserMoreThan30SecondsTest()
        {
            var user = new User() { UserId = "TestId1" };

            userManagementService.GetPasswordForUser(user);

            //hang the process for 31 second
            System.Threading.Thread.Sleep(31000); // this is making your test takes forever to run. Receive a Func<DateTime> or a IDateTimeProvider interface and mock it's value for what you want

            userManagementService.ValidatePasswordForUser(user);

            Assert.That(user.IsOTPasswordValid, Is.False);

        }

    }
}
