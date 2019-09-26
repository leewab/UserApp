var MySQL = require('./MySQLHelper');
var mySQL = new MySQL();

function AccountDataHandler(){

    var tablename = "account_web";

    /** 查询账户信息 */
    this.SearchAccountData = function(_req, _callBack){
        if(_req == null) return;
        mySQL.ResearchSwitch(_req.searchkey, tablename, _req.switch, function(_result){
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

    /** 添加账户信息 */
    this.AddAccountData = function(_req, _callBack){
        if(_req == null) return;
        var title = "name, password, weburl, webname, des";
        var data = _req.name + ',' + _req.password + ',' + _req.weburl + ',' + _req.webname + ',' + _req.des;
        mySQL.Insert(title, data, tablename, function(_result){
            if(_result != null){
                //添加成功
                if(_callBack != null) _callBack(true);
            }else{
                //添加失败
                if(_callBack != null) _callBack(false);
            }
        });
    }

    /** 移除账户信息 */
    this.RemoveAccountData = function(){

    }

}

module.exports = AccountDataHandler;