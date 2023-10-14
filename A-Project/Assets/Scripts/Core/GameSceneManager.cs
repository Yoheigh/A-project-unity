using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager instance;
    public static GameSceneManager Instance { get { return instance; } }

    public Dictionary<string, BaseScene> sceneDictionary = new Dictionary<string, BaseScene>();
    private string currentSceneName = null;

    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        GameObject _instance = GameObject.Find($"[{nameof(GameSceneManager)}]");
        if (_instance == null)
            _instance = new GameObject($"[{nameof(GameSceneManager)}]");

        instance = _instance.GetOrAddComponent<GameSceneManager>();
        DontDestroyOnLoad(instance.gameObject);

        Managers.Resource.LoadAllAsync<Object>("Preload", null, () =>
        {
            Debug.Log("·Îµù ³¡!");
            Managers.UI.ShowSceneUI<UIGameScene>();
            Managers.UI.SceneUI.Init();
        });
    }

    private void Awake()
    {

        if (currentSceneName == null)
        {
            currentSceneName = "Login";
        }
        else
        {
            currentSceneName = SceneManager.GetActiveScene().name;
        }

        //Managers.Data.LoadSceneData(Define.Scene.Pre);
        //SceneManager.sceneLoaded += instance.LoadedScene;
        //instance.LoadedScene(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        Managers.Game.Init();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public BaseScene GetSceneClass(string sceneName)
    {
        //if (sceneName == "Login") return gameObject.AddComponent<LoginScene>();
        //if (sceneName == "Lobby") return gameObject.AddComponent<LobbyScene>();

        return null;
    }

    public void RemoveSceneClass(string sceneName)
    {
        //if (sceneName == "Login") Destroy(GetComponent<LoginScene>());
        //if (sceneName == "Lobby") Destroy(GetComponent<LobbyScene>());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }

    public void RegisterScene(string sceneName)
    {
        currentSceneName = sceneName;
        if (!sceneDictionary.ContainsKey(sceneName))
        {
            BaseScene tempScene = GetSceneClass(sceneName);
            tempScene.sceneName = sceneName;
            sceneDictionary.Add(sceneName, tempScene);
        }
    }

    public BaseScene GetScene(string sceneName)
    {
        if (sceneDictionary.ContainsKey(sceneName))
        {
            return sceneDictionary[sceneName];
        }
        return null;
    }

    public void UnloadScene(string sceneName)
    {
        if (sceneDictionary.ContainsKey(sceneName))
        {
            sceneDictionary.Remove(sceneName);
            RemoveSceneClass(sceneName);
        }
    }

    public IEnumerator LoadSceneButton(string sceneName)
    {
        UnloadScene(currentSceneName);
        yield return new WaitForSeconds(0.5f);
        LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        RegisterScene(currentSceneName);
    }
}

public class BaseScene : MonoBehaviour
{
    public string sceneName;
    public int sceneIndex;
    public bool sceneDataShow;
    public string sceneData0, sceneData1;
    public string sceneType;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void Clear()
    {

    }
}
