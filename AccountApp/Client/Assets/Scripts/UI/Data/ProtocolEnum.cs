namespace UI.Manager
{
    public class ProtocolEnum
    {
        /// <summary>
        /// 用户模块 客户端请求动作
        /// </summary>
        public enum REQ_USER_ACTION
        {
            USER_ADD = 10005,
            USER_REMOVE = 10006,
            USER_GET = 10007,
            USER_LOGIN = 10008,
            USER_REGISTER = 10009,
        }
        
        /// <summary>
        /// 账户模块 客户端请求动作
        /// </summary>
        public enum REQ_ACCOUNT_ACTION
        {
            ACCOUNT_QUESTDATA = 20001,
            ACCOUNT_ADDDATA = 20002,
            ACCOUNT_REMOVEDATA = 20003,
        }
        
        /// <summary>
        /// 用户模块 服务器响应状态
        /// </summary>
        public enum RES_USER_STATE
        {
            USER_LOGIN_SUCCESS = 100001,         //用户登录成功
            USER_LOGIN_FAIL = 100002,            //用户登录失败（密码\验证码错误）
            USER_LOGIN_ERROE = 100003,           //用户登录出错（网络、数据错误）
            USER_LOGIN_NOUSER = 100004,          //用户名不存在
            
            USER_REGISTER_SUCCESS = 100010,      //用户注册成功
            USER_REGISTER_FAIL = 100011,         //用户注册失败
        }
        
        public enum RES_ACCOUNT_STATE
        {
            
        }
        

    }
}