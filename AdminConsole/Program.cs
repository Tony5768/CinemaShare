﻿using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AdminConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CinemaDbContext>();
            optionsBuilder.UseMySQL<CinemaDbContext>("server=localhost;port=3306;username=root;password=root;database=cinemaapp;SslMode = None");
            CinemaDbContext dbContext = new CinemaDbContext(optionsBuilder.Options);
            var user = new CinemaUser()
            {
                FirstName = "Tsvetilin",
                LastName = "Tsvetilov",
                UserName = "Ceco",
                CreatedOn = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Email="cvetilov6@abv.bg",
            };

            var hasher = new PasswordHasher<CinemaUser>();
            var password = hasher.HashPassword(user, "PasswordSecretToHashxD");
            user.PasswordHash = password;

            //IUserStore<CinemaUser> store = new CinemaStore();
            //IOptions<IdentityOptions> options = new CinemaOptions();
            //var userValidator = new[] { new UserValidator<CinemaUser>() };
            //var passwordValidator = new[] { new PasswordValidator<CinemaUser>() };
            //var manager = new UserManager<CinemaUser>(store, options, hasher, userValidator, passwordValidator, null, null, null, null);
            //await manager.CreateAsync(user, password);

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var getUser = await dbContext.Users.FirstOrDefaultAsync(x => x.LastName == "Tsvetilov");
            var name = user?.LastName;
            Console.WriteLine(name);
        }
    }

    //internal class CinemaOptions : IOptions<IdentityOptions>{ }
    //internal class CinemaStore : IUserPasswordStore<CinemaUser>{ }

}
