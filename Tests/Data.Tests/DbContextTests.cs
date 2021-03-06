﻿using CinemaShare.Models.InputModels;
using Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;

namespace Tests.Data.Tests
{
    class DbContextTests
    {
        public DbSet<FilmInputModel> DbSet { get;}

        [Test]
        public void FindDbSetReturnRightValue()
        {
            // Arange
            var mockContext = new Mock<CinemaDbContext>();
            
            mockContext.Setup(x => x.Find(It.IsAny<Type>())).Returns(DbSet);

            // Act
            var resultSet = (DbSet<FilmInputModel>)mockContext.Object.Find(typeof(DbSet<FilmInputModel>));

            // Assert
            mockContext.Verify(x => x.Find(It.IsAny<Type>()), Times.Once);
            Assert.AreEqual(DbSet, resultSet);
        }
    }
}
