using System;
using System.Collections.Generic;
using UI.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : Singleton<UIManager>
{
    #region UI常用API

    public static void OpenUI()
    {
        
    }

    public static void CloseUI()
    {
    }

    public static void PushUI()
    {
    }

    public static void BackUI()
    {
    }

    private UIRoot uiRoot = null;

    public UIRoot UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                uiRoot = Object.Instantiate(LoadUI("Prefabs/UI/UIRoot")).GetComponent<UIRoot>();
            }

            return uiRoot;
        }
    }

    public void Dispose()
    {
        ClearPool();
    }

    private string resPath = "Prefabs/UI/";

    public T OpenUI<T>(IBaseData data = null) where T : BaseUI
    {
        var t = GetUI<T>(resPath + typeof(T).Name, UIRoot.CommonPanel);
        t.InitUI(data);
        t.Show();
        return t;
    }

    public void CloseUI<T>(bool isDestory = false) where T : BaseUI
    {
        var t = GetUI<T>(resPath + typeof(T).Name);
        t.Close(isDestory);
    }

    #endregion

    private Dictionary<string, BaseUI> uiDic = new Dictionary<string, BaseUI>();

    private T GetUI<T>(string path, Transform parent = null) where T : BaseUI
    {
        BaseUI uiPanel = null;
        if (!uiDic.TryGetValue(path, out uiPanel))
        {
            uiPanel = GameObject.Instantiate(LoadUI(path), parent, false).GetComponent<T>();
            uiDic.Add(path, uiPanel);
        }

        return uiPanel as T;
    }

    private void ClearPool()
    {
        uiDic.Clear();
    }

    private GameObject LoadUI(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}