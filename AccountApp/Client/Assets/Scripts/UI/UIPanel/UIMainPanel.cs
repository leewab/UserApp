using System.Collections.Generic;
using DG.Tweening;
using UI;
using UI.Framework;
using UI.Manager;
using UI.UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class UIMainPanel : BaseUI
{
    [SerializeField] private Button btn_SignOut;
    [SerializeField] private Button btn_AddAccount;
    [SerializeField] private Button btn_Refresh;
    [SerializeField] private Text txt_UserName;
    [SerializeField] private Image img_Header;
    
    [SerializeField] private Transform tran_Content;
    [SerializeField] private GameObject obj_Item;

    private UserData userData = null;
    public List<AccountData> accountDatas = null;
    private bool isInited = false;

    private ContentSizeFitter contentSizeFitter;
    private List<GameObject> objList = new List<GameObject>();
    protected override void OnShow()
    {
        userData = UserDataManager.Instance.CurUserData;
        accountDatas = AccountDataManager.Instance.AccountDatas;
        txt_UserName.text = userData?.Username;
        if (accountDatas != null) isInited = true;
    }

    protected override void OnRefresh()
    {
        OnClose();
        OnShow();
    }

    protected override void OnClose()
    {
        ClearPanel();
    }

    protected override void RegisterUIEvent()
    {
       btn_SignOut.onClick.AddListener(OnClickSignOutEvent);
       btn_AddAccount.onClick.AddListener(OnClickAddAccountEvent);
       btn_Refresh.onClick.AddListener(OnClickRefreshEvent);
    }

    protected override void UnRegisterUIEvent()
    {
        btn_SignOut.onClick.RemoveListener(OnClickSignOutEvent);
        btn_AddAccount.onClick.RemoveListener(OnClickAddAccountEvent);
        btn_Refresh.onClick.RemoveListener(OnClickRefreshEvent);
    }

    private void OnClickSignOutEvent()
    {
        UIManager.Instance.CloseUI<UIMainPanel>();
        UserDataManager.Instance.CurUserData = null;
        UIManager.Instance.OpenUI<UILoginPanel>();
    }

    private void OnClickAddAccountEvent()
    {
        var uiAddAccountPanel = UIManager.Instance.OpenUI<UIAddAccountPanel>();
        uiAddAccountPanel.OnCloseEvent += () =>
        {
            Refresh();
        };
    }

    private void OnClickRefreshEvent()
    {
        Refresh();
    }

    private void ClearPanel()
    {
        txt_UserName.text = "";
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i] != null) Destroy(objList[i]);
        }
        objList.Clear();
    }

    private void Awake()
    {
        contentSizeFitter = tran_Content.GetComponent<ContentSizeFitter>();
    }

    private int sordIndex = 0;
    private void Update()
    {
        GenerateItem();
    }

    private void GenerateItem()
    {
        if (sordIndex >= accountDatas.Count)
        {
            isInited = false;
            return;
        }
        CreateItem(accountDatas[sordIndex]);
        sordIndex ++;
    }
    
    private void CreateItem(AccountData _data)
    {
        if (_data == null) return;
        var item = Instantiate(obj_Item, tran_Content, false).GetComponent<AccountItem>();
        objList.Add(item.gameObject);
        item.gameObject.SetActive(true);
        item.transform.DOScaleX(1, 0.2f);
        item.InitItem(new AccoutItemInfo()
        {
            data = _data,
            callBackShowSecret = r =>
            {
                contentSizeFitter.enabled = false;
                contentSizeFitter.enabled = true;
            },
            callBackDelete = () =>
            {
                Debug.Log("Delete");
                Refresh();
            }
        });
    }
}

