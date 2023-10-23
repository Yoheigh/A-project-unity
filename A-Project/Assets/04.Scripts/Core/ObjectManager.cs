using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using UnityEngine;
using static Define;

public class ObjectManager
{
    public HashSet<BaseController> Controllers { get; } = new HashSet<BaseController>();
    public PlayerController Player
    {
        get
        {
            if (player == null)
                player = GameObject.FindObjectOfType<PlayerController>();

            return player;
        }
    }
    private PlayerController player;

    public Transform ControllerRoot
    {
        get
        {
            GameObject root = GameObject.Find("@Monster");
            if (root == null)
                root = new GameObject { name = "@Monster" };

            return root.transform;
        }

    }

    // 생성자 호출하자마자 초기화하도록
    public ObjectManager()
    {
        Init();
    }

    public void Init()
    {

    }

    public void Clear()
    {
        Controllers.Clear();
    }

    public T Spawn<T>(Vector3 position) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(BaseController))
        {
            GameObject go = Managers.Resource.Instantiate("Monster", pooling: true);
            BaseController mc = go.GetOrAddComponent<BaseController>();
            go.transform.position = position;
            Controllers.Add(mc);
            return mc as T;
        }
        //else if(type == typeof(BoxController))
        //{
        //    GameObject go = Managers.Resource.Instantiate("Box", pooling: true);
        //    BoxController bc = go.GetOrAddComponent<BoxController>();
        //    go.transform.position = position;
        //    Boxs.Add(bc);
        //    return bc as T;
        //}


        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(BaseController))
        {
            Controllers.Remove(obj as BaseController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        //else if(type == typeof(BoxController))
        //{
        //    Boxs.Remove(obj as BoxController);
        //    Managers.Resource.Destroy(obj.gameObject);

        //    GameObject.Find("@Grid").GetOrAddComponent<GridController>().Remove(obj.gameObject);
        //}
    }
}
