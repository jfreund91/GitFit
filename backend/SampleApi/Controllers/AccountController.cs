using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.DAL;
using SampleApi.Models;
using SampleApi.Models.Account;
using SampleApi.Providers.Security;

namespace SampleApi.Controllers
{
    /// <summary>
    /// Creates a new account controller used to authenticate the user.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ITokenGenerator tokenGenerator;
        private IPasswordHasher passwordHasher;
        private IUserDAO userDao;

        /// <summary>
        /// Creates a new account controller.
        /// </summary>
        /// <param name="tokenGenerator">A token generator used when creating auth tokens.</param>
        /// <param name="passwordHasher">A password hasher used when hashing passwords.</param>
        /// <param name="userDao">A data access object to store user data.</param>
        public AccountController(ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IUserDAO userDao)
        {
            this.tokenGenerator = tokenGenerator;
            this.passwordHasher = passwordHasher;
            this.userDao = userDao;
        }

        /// <summary>
        /// Registers a user and provides a bearer token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register(AuthenticationModel model)
        {            
            // Does user already exist
            if (userDao.GetUser(model.Username) != null)
            {
                return BadRequest(new
                {
                    Message = "Username has already been taken."
                });
            }

            // Generate a password hash
            var passwordHash = passwordHasher.ComputeHash(model.Password);

            // Create a user object
            var user = new User { Password = passwordHash.Password, Salt = passwordHash.Salt, Role = "User", Username = model.Username };

            // Save the user
            userDao.CreateUser(user);

            // Generate a token
            var token = tokenGenerator.GenerateToken(user.Username, user.Role);

            // Return the token
            return Ok(token);
        }

        [HttpPut("updatePassword")]
        public IActionResult UpdatePassword(AuthenticationModel model)
        {
            var passwordHash = passwordHasher.ComputeHash(model.Password);

            var user = userDao;

            //userDao.UpdateUser(user);

            
            return Ok();
        }


        /// <summary>
        /// Authenticates the user and provides a bearer token.
        /// </summary>
        /// <param name="model">An object including the user's credentials.</param> 
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(AuthenticationModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            // Get the user by username
            var user = userDao.GetUser(model.Username);

            // If we found a user and the password has matches
            if (user != null && passwordHasher.VerifyHashMatch(user.Password, model.Password, user.Salt))
            {
                // Create an authentication token
                var token = tokenGenerator.GenerateToken(user.Username, user.Role);

                // Switch to 200 OK
                result = Ok(token);
            }

            return result;
        }

        /// <summary>
        /// Changes the user's password
        /// </summary>
        /// <param name="model">An object containing credentials and new password.</param> 
        /// <returns></returns>
        [HttpPost("changePassword")]
        public IActionResult ChangePassword(UpdatePasswordModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            // Get the user by username
            var user = userDao.GetUser(model.Username);

            // If we found a user and the password has matches
            if (user != null && passwordHasher.VerifyHashMatch(user.Password, model.Password, user.Salt))
            {

                // Update the password to the new password

                // Generate a password hash
                var passwordHash = passwordHasher.ComputeHash(model.NewPassword);

                // Create a user object
                var userUpdated = new User { Password = passwordHash.Password, Salt = passwordHash.Salt, Role = user.Role, Username = model.Username, Id = user.Id };

                // Update the password and the salt
                userDao.UpdateUser(userUpdated);

                // Generate an authentication token
                var token = tokenGenerator.GenerateToken(user.Username, user.Role);

                // Switch to 200 OK
                result = Ok(token);
            }
   
            return result;
        }


        [HttpGet]
        [Route("/test")]
        [Authorize]
        public ActionResult<string> Test()
        {
            return base.User.Identity.Name;
        }
    }
}