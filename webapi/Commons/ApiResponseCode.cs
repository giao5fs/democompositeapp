using System.ComponentModel;

namespace webapi.Commons
{
    public enum ApiResponseCode
    {
        [Description("Success")]
        None = 0,

        [Description("Access denied")]
        ERROR_ACCESS_DENY = 1,

        [Description("Email or Password incorrect")]
        LOGIN_ERROR = 2,

        [Description("Item is not existed")]
        ITEM_NOT_EXIST = 3,

        [Description("Parameter is not valid")]
        PARAMETER_IS_NOT_VALID = 4,

    }
}
