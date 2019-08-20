namespace IntegrationTests
{
    using System;
    using Xunit;
    using Settings;
    using DataAccess.Models;

    public class UserTests : IClassFixture<UserFixture>
    {
        private UserFixture userFixture;

        public UserTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;

        }

        [Fact]
        public void CanCreateNewUser()
        {
            var user = userFixture.User;
            user.Login = Guid.NewGuid().ToString();
            user.Password = Guid.NewGuid().ToString();
            user.Insert();
        }

        [Fact]
        public void CanFindUserByLogin()
        {
            var user = userFixture.User;
            var foundUser = User.Find(user.Login);

            Assert.Equal(user.Id, foundUser.Id);
            Assert.Equal(user.Login, foundUser.Login);
            Assert.Equal(user.Password, foundUser.Password);
        }
    }

    public class UserFixture: IDisposable
    {
        public User User { get; set; }

        public UserFixture()
        {
            var settings = new Settings
            {
                ConnectionString = @"Data Source=.\MSSQLSERVER2017;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;Initial Catalog=notes"
            };

            Settings.Initialize(settings);
            this.User = new User();
        }

        public void Dispose()
        {
            this.User.Delete();
        }
    }
}
