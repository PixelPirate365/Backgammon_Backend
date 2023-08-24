using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Common.Constants {
    public static class ResponseMessageConstants {
        public const string InvalidEmailPassword = "Invalid email or password";
        public const string SuccessfulyLogin = "User login successfully";
        public const string UserNotFound = "No user found";
        public const string InvalidPassword = "Please enter correct existing password";
        public const string UnableToChangePassword = "Unable to change password";
        public const string PasswordChangedSuccessfully = "Password changed successfully";
        public const string UserCreationFailed = "User not created successfully";
    }
}
