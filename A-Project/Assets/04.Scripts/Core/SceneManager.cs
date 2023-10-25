using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;
    public static SceneManager Instance { get { return instance; } }

    public Dictionary<string, BaseScene> sceneDictionary = new Dictionary<string, BaseScene>();
    private string currentSceneName = null;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void Init()
    {
        GameObject _instance = GameObject.Find($"[{nameof(SceneManager)}]");
        if (_instance == null)
            _instance = new GameObject($"[{nameof(SceneManager)}]");

        instance = _instance.GetOrAddComponent<SceneManager>();
        DontDestroyOnLoad(instance.gameObject);

        Managers.Resource.LoadAllAsync<Object>("Preload", (key, count, total) =>
                                              { Debug.Log($"{key} : {count} / {total}"); }, () =>
        {
            Debug.Log("·Îµù ³¡!");

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += Managers.Scene.OnSceneLoaded;
            Managers.Scene.OnSceneLoaded(UnityEngine.SceneManagement.SceneManager.GetActiveScene(), LoadSceneMode.Single);;
        });
    }

    private void Awake()
    {
        if (currentSceneName == null)
        {
            currentSceneName = "WorldScene";
        }
        else
        {
            currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        // Managers.Data.LoadSceneData(Define.Scene.Pre);
        // Managers.Game.Init();
    }

    //private void OnEnable()
    //{
    //    UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public BaseScene GetSceneClass(string sceneName)
    {
        if (sceneName == "WorldScene") return gameObject.AddComponent<WorldScene>();
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        RegisterScene(currentSceneName);
    }
}
