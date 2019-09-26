var AccountDataHandler = require('../Handler/AccountDataHandler');
var accountDataHandler = new AccountDataHandler();

function AccountModel(){

    /** 账户请求 */
    this.AccountRequest = function(req, res){
        var bodyInfo = req.body;
        switch(parseInt(bodyInfo.protocolevent))
        {
            case ProtocolEnum.ACCOUNTINFO.REQUESTDATAS:
                RequestDatas(bodyInfo);
                return false;
            case ProtocolEnum.ACCOUNTINFO.ADDDATA:
                AddData();
                return false;
            case ProtocolEnum.ACCOUNTINFO.REMOVEDATA:
                RemoveData();
                return false;
        }

        //请求数据
        function RequestDatas(){

        }

        //添加数据
        function AddData(){

        }

        //移除数据
        function RemoveData(){

        }
    }

    /** 账户请求数据 body.searchkey body.switch */
    this.AccountRequestDatas = function(req, res){
        var bodyInfo = req.body;
        accountDataHandler.SearchAccountData(bodyInfo.searchkey, bodyInfo.switch, function(_datas){
            var datasTxt = JSON.parse(_datas);
            console.log(datasTxt);
            //TODO:注意这里需要加密
            res.send(datasTxt);
        });
    }

    /** 添加账户数据 body.webname weburl webdes name password */
    this.AddAccountData = function(req, res){
        accountDataHandler.AddAccountData(req.body, function(_result){
            if(_result){
                //添加成功
                res.send(ProtocolEnum.ACCOUNTINFO.ADDDATA_SUCCESS);
            }else{
                //添加失败
                res.send(ProtocolEnum.ACCOUNTINFO.ADDDATA_FAIL);
            }
        });
    }

    /** 移除账户数据 body.id */
    this.RemoveAccountData = function(req, res){
        if(req.body == null) return;
        accountDataHandler.RemoveAccountData(req.body, function(_result){
            if(_result){
                //删除成功
                res.send(ProtocolEnum.ACCOUNTINFO.REMOVEDATA_SUCCESS);
            }else{
                //删除失败
                res.send(ProtocolEnum.ACCOUNTINFO.REMOVEDATA_FAIL);
            }
        });
    }
}

module.exports = AccountModel;