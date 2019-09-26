function MySQLHelper(){
    var mysql = require('mysql');

    var connection;
    var connInfo = {
            host : 'localhost',
            user : 'root',
            password : '123456',
            database : 'ibing_user'
        };
    // var connection = mysql.createConnection({
    //     host : 'localhost',
    //     user : 'root',
    //     password : '123456',
    //     datebase : 'ibing_user'
    // });

    this.ConnectionMySql = function(_connInfo, _callBack){
       connInfo = _connInfo;
       Connection(_connInfo, _callBack);
    }

    /** 执行SQL */
    this.Query = function(_sql){
        Query(_sql);
    }

    /** 查询数据 */
    this.Research = function(_title, _table, _callBack){
        var sql = "SELECT " + _title + " FROM " + _table;
        Query(sql, _callBack);
    }

    /** 查询数据 使用条件 */
    this.ResearchSwitch = function (_title, _table, _switch, _callBack) {
        var sql = "";
        if(_switch == null){
            sql = "SELECT " + _title + " FROM " + _table;
        }else{
            sql = "SELECT " + _title + " FROM " + _table + " WHERE " + _switch;
        }
        
        console.log(sql);
        Query(sql, _callBack);
    }

    /** 增加数据 */
    this.Insert = function(_title, _data, _table, _callBack){
        // addSql = "INSERT INTO user_account(username, password, idcard, phone) VALUES('ddddpppddddd', 'dd', '0123456789', '1234567890')";
        var sql = 'INSERT INTO ' + _table + '(' + _title + ') VALUES(' + _data + ')';
        console.log(sql);
        Query(sql, _callBack);
    }

    /** 更新数据 */
    this.Update = function(_table, _updateData, _switch){
        // var sql = "UPDATE user_account SET username = 'ooo', password = 'ppp' WHERE username = 'lee'";
        var sql = "UPDATE " + _table + " SET " + _updateData + " WHERE " + _switch;
        console.log(sql);
        Query(sql);
    }

    /** 移除数据 */
    this.Remove = function(_table, _switch){
        //var delSql = 'DELETE FROM websites where id=6';
        var sql = "DELETE FROM " + _table + " WHERE " + _switch;
        console.log(sql);
        Query(sql);
    }

    /** 执行SQL */
    function Query(_sql, _callBack){
        if(connection == null){
            console.error("Connecting");
            Connection(connInfo, function(){
                Query(_sql, _callBack);
            })
        }
        else
        {
            connection.query(_sql, function(_err, _result){
                if(_err){
                    console.log('[QUERY ERROR] --- ', _err.message);
                    if(_callBack != null) _callBack(null);
                    return;
                }

                console.log('----------------Query--------------');
                console.log(_result);
                console.log('-----------------------------------');
                if(_callBack != null) _callBack(_result);
            });
        }
    }

    function Connection(_sqlInfo, _callBack){
        connection = mysql.createConnection(_sqlInfo);
        connection.connect(function(_err){
            if(_err != null)
            {
                console.error(_err);
            }
            else
            {
                if(_callBack != null) _callBack();
            }
        });
    }

}

module.exports = MySQLHelper;