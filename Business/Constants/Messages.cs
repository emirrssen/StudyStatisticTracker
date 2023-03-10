using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        public static string UserRegistered = "User successfully registered!";
        public static string UserNotFound = "User not found!";
        public static string PasswordError = "Password is incorrect!";
        public static string SuccessfulLogin = "Login successfull!";
        public static string UserAlreadyExists = "User already exists!";
        public static string AccessTokenCreated = "Access token created successfully!";
        public static string AuthorizationDenied = "Authorization denied!";
    }
}
