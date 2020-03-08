using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineShop.Areas.Administrator.Controllers;
using Xunit;

namespace UnitTestovi
{
    public class ControllerTests
    {

        private Context _context { get; set; }
        private UserManager<AppUser> _userManager { get; set; }

        public ControllerTests()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new Context(options);

            _userManager = new UserManager<AppUser>(
                new UserStore<AppUser>(_context),
                null,
                new PasswordHasher<AppUser>(),
                new List<IUserValidator<AppUser>> {new UserValidator<AppUser>()},
                null, null, null, null,
                new Mock<ILogger<UserManager<AppUser>>>().Object);

        }

        [Fact]
        public async void Korisnici_PosaljiToken_Test()
        {

            
            //var expected = "error";
            //var controller = new KorisniciController(null, null);

            //var actual = await controller.PosaljiToken("20");



            //Assert.Equal(expected, actual);
        }
    }
}
