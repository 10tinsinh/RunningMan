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

        public const string CREATE_TEAM = "CREATE_TEAM";
        public const string TEAM_LEAD = "TEAM_LEAD";
        public const string TEAM_MEMBER = "TEAM_MEMBER";
        public const string JOIN_TEAM = "UPDATE_TEAM";


    }
    public static class PermissionCode
    {
        public const string RUNNING_MAN_USER_VIEW = "RUNNING_MAN_USER_VIEW";
        public const string RUNNING_MAN_USER_CREATE = "RUNNING_MAN_USER_CREATE";
        public const string RUNNING_MAN_USER_UPDATE = "RUNNING_MAN_USER_UPDATE";
        public const string RUNNING_MAN_USER_DELETE = "RUNNING_MAN_USER_DELETE";

        public const string RUNNING_MAN_TEAM_LEADER = "RUNNING_MAN_TEAM_LEADER";
        public const string RUNNING_MAN_TEAM_MEMBER = "RUNNING_MAN_TEAM_MEMBER";
        public const string RUNNING_MAN_TEAM_CREATE = "RUNNING_MAN_TEAM_CREATE";
        public const string RUNNING_MAN_TEAM_JOIN = "RUNNING_MAN_TEAM_JOIN";



    }
}
