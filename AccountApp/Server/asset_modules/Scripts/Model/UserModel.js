var UserDataHandler = require('../Handler/UserDataHandler');
var userDataHandler = new UserDataHandler();

function UserModel(){

    /** 注册用户 */
    this.RegistUser = function(req, res){
        var bodyInfo = req.body;
        console.log(req);
        console.log(bodyInfo);
        CheckAccount_Username(bodyInfo.username, function(_isCreate){
            //可以创建
            if(_isCreate){
                CreateAccount(bodyInfo, function(_createResult){
                    if(_createResult == null){
                        console.error('新增账户失败');
                        //{"Id":90983872,"Data":{"UserDatas":[{"Username":"000","Password":"999","PhoneNo":7654,"IdCard":88,"Remark":null}]}}
                        var result = "{'Id':" + ProtocolEnum.RES_USER_STATE.USER_REGISTER_FAIL + ", 'Data': 'null'}";
                        res.end(result);
                    }
                    else
                    {
                        console.error('新增账户成功');
                        var result = "{\"Id\":" + ProtocolEnum.RES_USER_STATE.USER_REGISTER_SUCCESS +
                         ", \"Data\":{\"UserDatas\":[{\"Username\":\""+ bodyInfo.username +"\",\"Password\":\"" + bodyInfo.password + "\",\"PhoneNo\":\"7654\",\"IdCard\":\"88\",\"Remark\":null}]}}";
                        res.end(result);
                    }
                });
            }
            //不可以创建
            else
            {
                console.error('已经有同名的用户名, 不可以创建');
                res.end("fail");
            }
        });
    }

    /** 检查账户的用户名username 回调是否可以创建 */
    function CheckAccount_Username(_username, _callBack) {
        userDataHandler.FindUserAccountWithName(_username, function (_result) {
            if(_result == null){
                console.log("该账户没有，可以创建");
                if (_callBack != null) _callBack(true);
            }else{
                console.log("该账户有一个，不可以创建");
                if (_callBack != null) _callBack(false);
            }
        });
    }

    /** 创建账户 */
    function CreateAccount(_bodyInfo, _callBack) {
        var accountInfo = {
            username: _bodyInfo.username,
            password: _bodyInfo.password,
            idcard: "999999999",
            phone: "110"
        };
        userDataHandler.CreateAccount(accountInfo, _callBack);
    }


    /** 登录用户 */
    this.LoginUser = function(req, res){
        userDataHandler.FindUserAccountWithName(req.body.username, function (_result) {
            if(_result == null){
                console.log("该账户没有,登录失败");
                res.send(ProtocolEnum.USER_LOGIN.LOGINSUCCESS);
            }else{
                console.log("该账户有一个");
                userDataHandler.CheckOutUserData(req.body, _result, function(_protocolEvent){
                    console.log(_protocolEvent);
                    res.send(_protocolEvent.toString());
                })
            }
        });
    }
}

module.exports = UserModel;