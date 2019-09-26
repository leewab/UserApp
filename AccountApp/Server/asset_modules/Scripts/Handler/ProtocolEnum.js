var ProtocolEnum = {

    /** 用户登录协议 */
    USER_LOGIN : {
        LOGINSUCCESS : 10000,          //登录成功
        PASSEORD_ERROR : 10001,        //密码错误
        CODE_ERROR : 10002,            //验证码错误
        USERNAME_NO : 10003,           //用户名没有
    },

    /** 用户注册协议 */
    USER_REGIST : {
        REGISTSUCCESS : 20000,         //注册成功
        USERNAME_REPEAT : 20001,       //用户名重复
    },

    /** 账户信息 */
    ACCOUNTINFO : {
        REQUESTDATAS : 30000,          //请求数据
        ADDDATA_SUCCESS : 30001,       //添加数据
        ADDDATA_FAIL : 30002,          //添加数据
        REMOVEDATA_SUCCESS : 30003,    //移除数据
        REMOVEDATA_FAIL : 30004,       //移除数据
    }
}

global.ProtocolEnum = ProtocolEnum;