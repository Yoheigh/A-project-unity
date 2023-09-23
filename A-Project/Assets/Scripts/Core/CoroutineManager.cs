using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static MonoBehaviour monoInstance;

    private static CoroutineManager instance;
    public static CoroutineManager Instance { get { return instance; } }

    private bool init = false;

    public void Init()
    {
        if(init == false)
        {
            init = true;

            monoInstance = new GameObject($"[{nameof(CoroutineManager)}]").AddComponent<CoroutineManager>();
            DontDestroyOnLoad(monoInstance.gameObject);
        }
    }

    public new static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return monoInstance.StartCoroutine(coroutine);
    }

    public new static void StopCoroutine(Coroutine coroutine)
    {
        monoInstance.StopCoroutine(coroutine);
    }
   
   
}
