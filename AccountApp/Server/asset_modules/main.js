var express = require('express');
// var multipart = require('connect-multiparty');
// var multiparty = multipart();
// var exStatic=require("express-static");
var bodyParser = require('body-parser');        //www-form-urlencoded 提交方式 默认提交方式
// var multer = require('multer');

//声明显示模块
require('./Scripts/Handler/ProtocolEnum');

console.log('------');
//用户模块
var UserModel = require('./Scripts/Model/UserModel');
var userModel = new UserModel();

var app = express();
// app.use(exStatic('./'));//这一句中的'./'是静态页面的相对路径。
app.use('/public', express.static('public'));
app.use(bodyParser.urlencoded({ extended: true }));
// app.use(multer({ dest: '/tmp/' }).array('pmForm'));

/** 用户注册 */
app.post('/ua/register', function (req, res){
  console.log('注册用户');
  userModel.RegistUser(req, res);
});

/** 用户注册 */
app.post('/ua/login', function (req, res){
  console.log('登录用户');
  userModel.LoginUser(req, res);
});

app.get('./ua/index', function (req, res){
  
})

var server = app.listen(8081, function () {
  var host = server.address().address
  var port = server.address().port
  console.log("应用实例，访问地址为 http://%s:%s", host, port)
});