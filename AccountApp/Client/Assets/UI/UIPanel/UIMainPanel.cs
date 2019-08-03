using System.Collections.Generic;
using DG.Tweening;
using UI;
using UI.Framework;
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
    private ContentSizeFitter contentSizeFitter;
    private List<GameObject> objList = new List<GameObject>();
    protected override void OnShow()
    {
        userData = DataManager.Instance.GetUserData();
        contentSizeFitter = tran_Content.GetComponent<ContentSizeFitter>();
        txt_UserName.text = DataManager.Instance.CurUsername;
        InitPanel();
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

    private void InitPanel()
    {
        if (userData == null || userData.AccountDatas == null) return;
        int count = userData.AccountDatas.Count;
        for (int i = 0; i < count; i++)
        {
            queue.Enqueue(userData.AccountDatas[i]);
        }
    }

    private void OnClickSignOutEvent()
    {
        UIManager.Instance.CloseUI<UIMainPanel>();
        DataManager.Instance.CurUsername = null;
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

    private void ClearPanel()
    {
        txt_UserName.text = "";
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i] != null) Destroy(objList[i]);
        }
        objList.Clear();
    }

    private Queue<AccountData> queue = new Queue<AccountData>();
    private float time = 0;
    private void Update()
    {
        time += 0.1f;
        GenerateItem(time);
    }

    private void GenerateItem(float value)
    {
        if (value < 3 || queue.Count <= 0) return; 
        CreateItem(queue.Dequeue());
    }
}

