namespace IntegrationTests
{
    using System;
    using Xunit;
    using Settings;
    using DataAccess.Models;

    public class UserTests
    {
        public UserTests()
        {
            var settings = new Settings
            {
                ConnectionString = @"Data Source=.\MSSQLSERVER2017;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;Initial Catalog=notes"
            };

            Settings.Initialize(settings);
        }

        [Fact]
        public void CreateNewUser()
        {
            var user = new User();
            user.Login = Guid.NewGuid().ToString();
            user.Password = Guid.NewGuid().ToString();
            user.Insert();
        }
    }
}
