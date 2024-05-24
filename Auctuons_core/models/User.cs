using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.models
{
    public class User
    {
        public Guid Id { get; }
        public string UserName { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public int? Age { get; } = 0;
        public string ?Aboutme { get; }
        public string ?AboutCompany { get; }
        public string? FirstName { get; }
        public string? SurName{ get; }
        protected User(Guid id ,string userName,string email,string passwordHash,string firstname,string surname, int? age = 0, string? aboutme = null, string? aboutcompany = null) {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Aboutme=aboutme;
            Age = age;
            AboutCompany = aboutcompany;
            FirstName = firstname;
            SurName = surname;
        }
        protected User(Guid id, string userName, string firstname, string surname, int? age = 0, string? aboutme = null, string? aboutcompany = null)
        {
            Id = id;
            UserName = userName;
            Aboutme = aboutme;
            Age = age;
            AboutCompany = aboutcompany;
            FirstName = firstname;
            SurName = surname;
        }

        public static Result<User> Create(Guid id, string userName, string email, string passwordHash, string? firstname=null, string? surname=null, int? age=0,string? aboutme=null,string?aboutcompany=null)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>("fields is cannot be nul");
            var user = new User(id, userName, email, passwordHash,  firstname, surname, age, aboutme,aboutcompany);
            return Result.Success(user) ;
        }
        public static Result<User> CreateProfile(Guid id, string userName,  string firstname , string surname , int age , string aboutme , string aboutcompany)
        {
            
            var user = new User(id, userName, firstname, surname, age, aboutme, aboutcompany);
            return Result.Success(user);
        }
    }
}
