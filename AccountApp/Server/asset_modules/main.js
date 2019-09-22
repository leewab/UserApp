var express = require('express');
var multipart = require('connect-multiparty');
var multiparty = multipart();
// var exStatic=require("express-static");
// var bodyParser = require('body-parser');
// var multer = require('multer');

//声明显示模块
var ViewModel = require('./Scripts/Model/ViewModel');
var viewModel = new ViewModel();

var app = express();
// app.use(exStatic('./'));//这一句中的'./'是静态页面的相对路径。
app.use('/public', express.static('public'));
// app.use(bodyParser.urlencoded({ extended: false }));
// app.use(multer({ dest: '/tmp/' }).array('pmForm'));

app.get('/ua', function (req, res) {
  console.log(req.url);
  viewModel.DisplayWebIndex(req, res);
});

app.get('/ua/domain', function (req, res) {
  console.log(req.url);
  viewModel.DisplayWebIndex(req, res);
});