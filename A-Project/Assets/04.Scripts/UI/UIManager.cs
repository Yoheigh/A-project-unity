using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class UIManager
{
    int _order = 10;
    int _toastOrder = 500;

    Stack<UIPopup> _popupStack = new Stack<UIPopup>();
    Queue<UIToast> _toastQueue = new Queue<UIToast>();
    UIScene _sceneUI = null;
    UIToast _subtitleUI = null;
    public UIScene SceneUI { get { return _sceneUI; } }

    public event Action<int> OnTimeScaleChanged;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true, int sortOrder = 0, bool isToast = false)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
        }

        CanvasScaler cs = go.GetOrAddComponent<CanvasScaler>();
        if (cs != null)
        {
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(1920, 1080);
        }

        go.GetOrAddComponent<GraphicRaycaster>();

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = sortOrder;
        }

        if (isToast)
        {
            _toastOrder++;
            canvas.sortingOrder = _toastOrder;
        }

    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null, bool pooling = true) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}", parent, pooling);
        go.transform.SetParent(parent);
        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        RefreshTimeScale();

        return popup;
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UIPopup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
        RefreshTimeScale();
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public T ShowSubtitle<T>(string msg) where T : UIToast
    {
        if (_subtitleUI != null)
            Managers.Resource.Destroy(_subtitleUI.gameObject);

        GameObject go = Managers.Resource.Instantiate($"{typeof(T).Name}");
        T toast = Util.GetOrAddComponent<T>(go);
        _subtitleUI = toast;

        _subtitleUI.SetInfo(msg);
        go.transform.SetParent(Root.transform);

        DOVirtual.DelayedCall(5f, () =>
        {
            if (toast != null)
            {
                Managers.Resource.Destroy(toast.gameObject);
            }
        });
        return toast;
    }

    public T ShowToast<T>(string name = null, string msg = null) where T : UIToast
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}");
        T toast = Util.GetOrAddComponent<T>(go);
        _toastQueue.Enqueue(toast);

        toast.SetInfo(msg);
        go.transform.SetParent(Root.transform);

        DOVirtual.DelayedCall(5f, () => Managers.UI.CloseToastUI());
        return toast;
    }

    public UIToast ShowToast(string msg)
    {
        string name = typeof(UIToast).Name;
        GameObject go = Managers.Resource.Instantiate($"{name}", pooling: true);
        UIToast popup = Util.GetOrAddComponent<UIToast>(go);
        popup.SetInfo(msg);
        _toastQueue.Enqueue(popup);
        go.transform.SetParent(Root.transform);
        CoroutineManager.StartCoroutine(CoCloseToastUI());
        return popup;
    }

    IEnumerator CoCloseToastUI()
    {
        yield return new WaitForSeconds(1f);
        CloseToastUI();
    }

    public void CloseToastUI()
    {
        if (_toastQueue.Count == 0)
            return;

        UIToast toast = _toastQueue.Dequeue();
        Managers.Resource.Destroy(toast.gameObject);
        toast = null;
        _toastOrder--;
    }

    public int GetPopupCount()
    {
        return _popupStack.Count;
    }

    public void Clear()
    {
        CloseAllPopupUI();
        Time.timeScale = 1;
        _sceneUI = null;
    }

    public void RefreshTimeScale()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != Define.Scene.GameScene.ToString())
        {
            Time.timeScale = 1;
            return;
        }

        if (_popupStack.Count > 0 || IsActiveSoulShop == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        DOTween.timeScale = 1;
        OnTimeScaleChanged?.Invoke((int)Time.timeScale);
    }


    bool _isActiveSoulShop = false;
    public bool IsActiveSoulShop
    {
        get { return _isActiveSoulShop; }
        set
        {
            _isActiveSoulShop = value;

            if (_isActiveSoulShop == true)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;

            DOTween.timeScale = 1;
            OnTimeScaleChanged?.Invoke((int)Time.timeScale);
        }
    }
}
