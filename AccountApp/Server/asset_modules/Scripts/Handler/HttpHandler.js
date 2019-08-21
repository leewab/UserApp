//声明FileHandler
var FileHandler = require('./FileHandler');
var fileHandler = new FileHandler();
//生命北京时间
const chinaTime = require('china-time');

var url = require('url');
var path = require('path')

var bodyInfo = [{}];
var filePath = "";           //这里存储的是后半截路径 需要通过服务器的前半截拼接

function HttpHandler() {

    /**
     * Post请求
     */
    this.Post = function (req, res) {

        //TODO:此处用来判断Post提交的类型 然后传递给不同处理器

        // console.log("------------");
        // console.log(req.files[0]);  // 上传的文件信息
        // bodyInfo = req.body;
        // filePath = ("/upload/" + req.files[0].originalname).split(path.sep).join('/');;
        // console.log(filePath);
        // var file_name = req.files[0].originalname;
        // resInfo = {
        //     message: 'File uploaded successfully',
        //     filename: file_name
        // };

        // fileHandler.ReadFile(req.files[0].path, ReadFileCallBack);

        // function ReadFileCallBack(data){
        //     console.log('Post // ReadFileCallBack');
        //     fileHandler.WriteFile(filePath, data, WriteFileCallBack);
        // }

        // function WriteFileCallBack(data){
        //     console.log('Post // WriteFileCallBack');
        //     UpdateLogInfo(GenerateLogInfo(file_name, filePath), UpdateLogInfoCallBack);
        // }

        // function UpdateLogInfoCallBack(){
        //     console.log("UpdateLogInfo finished");
        //     UpdatePluginInfo(UpdateVersionInfoCallBack);
        // }

        // function UpdateVersionInfoCallBack(){
        //     ResposeCallBack();
        // }

        // function ResposeCallBack(){

        //     res.end(JSON.stringify(resInfo));
        // }
    }

    /**
     * Get请求
     */
    this.Get = function (req, res) {

        //TODO:此处用来判断Post提交的类型 然后传递给不同处理器

        // //----------dao这里了  这里没有写
        // if(req.url === "/pm/download"){
        //     DownloadProgress(req, res);
        // }else if(req.url === "/pm/display"){
        //     console.log("/pm/display");
        //     DisplayProgress(req, res);
        // }else{
        //     IndexProgress(req, res);
        // }
    }

    function IndexProgress(req, res){
        // 解析 url 参数
        // var params = url.parse(req.url, true).query;
        // res.write("操作方式：" + params.operation);          //0 clear清除
        // res.write("操作文件：" + params.file);               //0 clear清除
        // var fPath = txt_commonPath + params.file;
        // if(params.operation === "0"){
        //     fileHandler.ClearFile(fPath, function(){
        //         res.end("clear success!");
        //     });
        // }
    }

    function DownloadProgress(req, res){
         // 解析 url 参数
         var params = url.parse(req.url, true).query;
         res.write("下载文件的Id：" + params.fileId);
         //TODO: 这里查询下载文件的路径并返回

    }

    function DisplayProgress(req, res) {
        // 解析 url 参数
        // var params = url.parse(req.url, true).query;
        //TODO: 这里查询Pluging的信息
        console.log("./" + filePath);
        res.sendfile("./" + filePath);
    }
}

var txt_commonPath = "E:/iPro/Node.js/PMServer/PMServer.git/trunk/Server/";
var txt_uploadLog = "E:/iPro/Node.js/PMServer/PMServer.git/trunk/Server/UploadLog.txt";
var txt_uploadVersion = "E:/iPro/Node.js/PMServer/PMServer.git/trunk/Server/PluginInfo.json";

/**
 * 更新log信息
 * @param {*} data
 * @param {*} callBack
 */
function UpdateLogInfo(data, callBack) {
    fileHandler.ReadFile(txt_uploadLog, ReadFileCallBack);

    function ReadFileCallBack(_data) {
        if (_data.length <= 0) {
            fileHandler.WriteFile(txt_uploadLog, data, WriteFileCallBack);
        } else {
            fileHandler.WriteFile(txt_uploadLog, _data.toString() + "\n" + data, WriteFileCallBack);
        }
    }

    function WriteFileCallBack(data) {
        if(callBack != null) callBack();
    }
}

/**
 * 获取log信息
 * @param {*} filename
 * @param {*} filePath
 */
function GenerateLogInfo() {
    return chinaTime('YYYY-MM-DD HH:mm:ss') + "   " + bodyInfo.version + "    " + bodyInfo.name + "    " + filePath;
}

/**
 * 更新Version信息
 * @param {*} data
 * @param {*} callBack
 */
var id = 1;
function UpdatePluginInfo(callBack){
    fileHandler.ReadFile(txt_uploadVersion, ReadFileCallBack);
    id++;
    var initInfo = [{id: id, version: bodyInfo.version, name: bodyInfo.name, title: bodyInfo.name, path:filePath, des: bodyInfo.des}];
    function ReadFileCallBack(_data) {
        if (_data.length <= 0) {
            fileHandler.WriteFile(txt_uploadVersion, JSON.stringify(initInfo), WriteFileCallBack);
        } else {
            var oInfos = null;
            console.log(_data.toString());
            if(_data != null) oInfos = JSON.parse(_data.toString());
            var onInfoStr = GeneratePluginInfo(oInfos, initInfo);
            fileHandler.WriteFile(txt_uploadVersion, JSON.stringify(onInfoStr), WriteFileCallBack);
        }
    }

    function WriteFileCallBack(data) {
        if(callBack != null) callBack();
    }
}

/**
 * 生成Version信息
 * @param {*} _id
 * @param {*} _version
 * @param {*} _name
 * @param {*} _title
 * @param {*} _des
 */
function GeneratePluginInfo(vis, nvi){
    if(vis == null) vis = [{}];
    if(nvi == null) nvi = [{}];
    return vis.concat(nvi);
}

module.exports = HttpHandler;