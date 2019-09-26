namespace UI
{
    public static class DefineManager
    {
        public static string port = "45419";                   //端口动态获取 不固定
        public static string ip = "ibinggame.ticp.io";                   //端口动态获取 不固定
        public static string url_IsUser = $"{ip}:{port}/login_Check?usename={0}";
        public static string url_AddUser = $"{ip}:{port}/login_Add";
        public static string url_IsLoginSuccess = $"{ip}:{port}/login";
        
        public static string url_RequestAccountData = $"{ip}:{port}/ua/data";
    }
}