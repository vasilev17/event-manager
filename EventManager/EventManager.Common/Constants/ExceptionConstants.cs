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
        public const string FailedToCreate = "Could not create {0}!";
        public const string AlreadyExists = "This {0} already exists!";
        public const string FailedToDelete = "Could not delete {0}!";
        public const string NotFound = "{0} not found!";
        public const string FailedToUpload = "Could not upload {0}!";
        public const string FailedToUpdate = "Could not update {0}!";


        //Event Validation Exceptions
        public const string InvalidEventDataInput = "The data you have entered seems invalid!";
        public const string InvalidActivityDateTimes = "Activities can not have start and/or end dates/times!";
        public const string InvalidEventDateTimes = "Events must have start and end dates/times! If there are no such consider creating an activity.";
        public const string InvalidRatingValue = "The rating value should be between 0 and 5";
        public const string InvalidRatingValueStep = "The rating value step is 0.5";
        public const string InvalidRatingTime = "The event must have started in order to be rated!";

        //Controller Exceptions
        public const string PictureNotUploaded = "Required field Picture is not uploaded!";

    }
}
