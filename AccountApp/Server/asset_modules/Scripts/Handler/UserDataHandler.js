var MySQL = require('./MySQLHelper');
var mySQL = new MySQL();

function UserDataHander(){

    /** 通过用户名查询用户账户 */
    this.FindUserAccountWithName = function(_username, _callBack){
        if(_username == null) return;
        mySQL.ResearchSwitch("*", "user_lib", "username = '" + _username.trim() + "'", function(_result){
            console.error(_result);
            if(_result != null && _result.length > 0){
                //表示有一个账户
                if(_callBack != null) _callBack(_result);
            }
            else
            {
                //表示没有账户
                if(_callBack != null) _callBack(null);
            }
        });
    }



    /** 通过身份证号码查询用户账户 */
    this.FindUserAccountWithIdCard = function(_idCard){

    }

    /** 通过电话号码查询用户账户 */
    this.FindUserAccountWithPhone = function(_phone){

    }

    /** 创建一个UserAccount账户 */
    this.CreateAccount = function(_userAccountTable, _callBack){
        if(_userAccountTable == null) return;
        var title = "username, password, idcard, phone";
        var data = "'" + _userAccountTable.username + "', '" + _userAccountTable.password + "', '" + _userAccountTable.idcard + "', '" + _userAccountTable.phone + "'";
        var table = "user_lib";
        console.log(data);
        mySQL.Insert(title, data, table, _callBack);
    }

    this.Trim = function(_str){
        var trimLeft = /^\s+/,
        trimRight = /\s+$/;
        _str.replace(trimLeft, "").replace(trimRight, "");
    }

    /** 校验用户数据 */
    this.CheckOutUserData = function(_bodyInfo, _serverResult, _callBack){
        console.log(_bodyInfo.password);
        console.log(_serverResult[0].password);
        var protocolEvent = 0;
        if(_bodyInfo.password == _serverResult[0].password){
            console.log("密码相同");
            protocolEvent = ProtocolEnum.USER_LOGIN.LOGINSUCCESS;
        }else{
            console.log("密码不相同");
            protocolEvent = ProtocolEnum.USER_LOGIN.PASSEORD_ERROR;
        }

        if(_callBack != null) _callBack(protocolEvent);
    }
}

module.exports = UserDataHander;