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
        public const string TokenNotExpired = "This token is not expired yet";
        public const string RefreshTokenExpired = "This refresh token has expired";
        public const string TokenNotExist = "This token does not exist";
        public const string InvalidateRefreshToken = "This refresh token has been invalidated";
        public const string UsedRefreshToken = "This refresh token has been used";
        public const string RefreshTokenNotMatchJWT = "This refresh token does not match JWT";
        public const string SuccefulyDeleted = "User deleted successfully";
        public const string UserDeactivated = "User is deactivated";



    }
}
