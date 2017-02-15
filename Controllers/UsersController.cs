using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogReg.Factory;
using LogReg.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LogReg.Controllers{
    public class UsersController : Controller{
        private readonly UserFactory userFactory;

        public UsersController(UserFactory user){
            userFactory = user;
        }
        
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            System.Console.WriteLine("IN INDEX");
            ViewBag.errors = new List<User>();
            ViewBag.lerrors = "hi";
            return View();
        }


        // POST: /Register/
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User NewUser){
            if(ModelState.IsValid){
                System.Console.WriteLine("CLEAN: INPUTS VALID");

                //ADD USER
                System.Console.WriteLine(NewUser.Password);
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                System.Console.WriteLine(NewUser.Password);
                userFactory.AddUser(NewUser);
                
                // GET USER
                ViewBag.user = userFactory.GetUser(NewUser.Email);

                return View("Success");
            } else {
                System.Console.WriteLine("ERROR: INPUTS INVALID");
                ViewBag.errors = ModelState.Values;
                ViewBag.lerrors = "";
                return View("Index");
            }
        }


        // POST: /Register/
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string PwToCheck){
            var user = userFactory.GetUser(email);

            if(user != null && PwToCheck != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, PwToCheck)){
                    ViewBag.user = user;
                    return View("Success");
                } else {
                    ViewBag.lerrors = "Invalid Combination";
                    ViewBag.errors = new List<User>();
                    return View("Index");
                }
            } else {
                ViewBag.errors = new List<User>();
                ViewBag.lerrors = "Invalid Combination";
                return View("Index");
            }
        }
    }
}
