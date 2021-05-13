var MySQL = require('./Scripts/Handler/MySQLHelper');
var mySQL = new MySQL();

var title = "username, password, idcard, phone";
var data = "'_userAccountTable.username', '_userAccountTable.password', '_userAccountTable.idcard', '_userAccountTable.phone'";
var table = "user_lib";
console.log(data);
mySQL.Insert(title, data, table, function(){
    console.log("Success");
});