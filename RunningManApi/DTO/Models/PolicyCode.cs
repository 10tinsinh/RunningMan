using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public static class PolicyCode
    {
        public const string VIEW_USER = "VIEW_USER";
        public const string DELETE_USER = "DELETE_USER";
        public const string ADMIN = "ADMIN";
        public const string USER = "USER";
        public const string UPDATE_USER = "UPDATE_USER";
        
    }
    public static class PermissionCode
    {
        public const string RUNNING_MAN_USER_VIEW = "RUNNING_MAN_USER_VIEW";
        public const string RUNNING_MAN_USER_CREATE = "RUNNING_MAN_USER_CREATE";
        public const string RUNNING_MAN_USER_UPDATE = "RUNNING_MAN_USER_UPDATE";
        public const string RUNNING_MAN_USER_DELETE = "RUNNING_MAN_USER_DELETE";

    }
}
