namespace UI
{
    public static class DefineData
    {
        #region URL

        public static string port = "8081";//"45419";                   //端口动态获取 不固定
        public static string ip = "http://127.0.0.1";//"ibinggame.ticp.io";                   //端口动态获取 不固定
        public static string url_GetUser = $"{ip}:{port}/login_Check?usename={0}";
        public static string url_AddUser = $"{ip}:{port}/ua/register";
        public static string url_UserLogin = $"{ip}:{port}/ua/login";
        
        public static string url_RequestAccountData = $"{ip}:{port}/ua/account";
        public static string url_AddAccountData = $"{ip}:{port}/ua/add_account";
        public static string url_RemoveAccountData = $"{ip}:{port}/ua/remove_account";

        #endregion
    }
}