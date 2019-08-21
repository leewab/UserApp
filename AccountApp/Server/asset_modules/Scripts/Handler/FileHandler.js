var fs = require('fs');

function FileHandler() {

    /**
     * 文件读取
     */
    this.ReadFile = function (path, callBack) {
        fs.readFile(path, "utf8", function (err, data) {
            if (err) {
                console.log(err);
            }
            console.log(data);
            if(callBack != null) callBack(data);
        });
    }

    /**
     * 文件写入
     */
    this.WriteFile = function (path, data, callBack) {
        fs.writeFile(path, data, function (err) {
            if (err) {
                return console.error(err);
            }
            if(callBack != null) callBack(data);
        });
    }

    /**
     * 文件清除
     */
    this.ClearFile = function (path, callBack){
        fs.writeFile(path, "", function(err){
            if(err){
                return console.error(err);
            }
            if(callBack != null) callBack();
        })
    }

    /**
     * fs关闭
     */
    this.Stop = function () {
        if(fs == null) return;
        // 关闭文件
        fs.close(function (err) {
            if (err) {
                console.log(err);
            }
            console.log("文件关闭成功");
        });
    }
}

module.exports = FileHandler;