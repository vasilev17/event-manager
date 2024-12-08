﻿namespace EventManager.Common.Constants
{
    public static class ExceptionConstants
    {
        //User Validation Exceptions
        public const string InvalidEmailFormat = "Email: \"{0}\" is not in correct format!";
        public const string InvalidPasswordSize = "Password must at least 8 charecters long!";
        public const string PasswordSpecialCharecters = "Password must meet at least 3 of the requierments!";
        public const string AllPropertiesRequiered = "All properties are needed!";
        public const string InvalidCredentials = "Thre credentials were incorrect!";
        public const string PasswordMissMatch = "The password and the confirmation do not match";
        public const string InvalidRole = "Invalid role is provided!";
        public const string Unauthorized = "User is unauthorized";
        public const string UnauthorizedThirdPartyCreation = "Role unauthorized to create third party events/activities!";

        //Service Exceptions
        public const string CantSendEmail = "Could not send email!";
        public const string PasswordToken = "Failed to generate password token!";
        public const string FailedToRestorePassword = "Failed to reset the password!";
        public const string CloudinaryError = "Cloudinary error: \"{0}\"";

        //Database exceptions
        public const string CanNotCreate = "Can not create {0}!";
        public const string AlreadyExists = "This {0} already exists!";
        public const string CantAddToRole = "Failed to add user to role!";
        public const string UserNotFound = "User not found!";
        public const string EventNotFound = "Event not found!";
        public const string FailedToDeleteUser = "Could not delete user!";
        public const string FailedToUpdateUser = "Could not update user!";
        public const string FailedToUpdateEvent = "Could not update event!";
        public const string FailedToUploadProfilePicture = "Could not upload profile picture!";
        public const string FailedToUploadEventPicture = "Could not upload event picture!";
        public const string FailedToDeleteEvent = "Could not delete event!";

        //Event Validation Exceptions
        public const string InvalidEventDataInput = "The data you have entered seems invalid!";
        public const string InvalidActivityDateTimes = "Activities can not have start and/or end dates/times!";
        public const string InvalidEventDateTimes = "Events must have start and end dates/times! If there are no such consider creating an activity.";

        //Controller Exceptions
        public const string PictureNotUploaded = "Required field Picture is not uploaded!";

    }
}
