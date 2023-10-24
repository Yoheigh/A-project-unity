using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager
{
    //Entity_ItemData entity_ItemData;

    //public void Init()
    //{
    //    //입력한 액샐값 ScriptableObject로 변환된 데이터를 로딩하기 위해서 
    //    entity_ItemData = (Entity_ItemData)Managers.Resource.Load<ScriptableObject>("ItemData");

    //    for (int i = 1; i < 5; i++)     //index가 1번 부터 있기 때문
    //    {
    //        Entity_ItemData.Param param = GetItemDataByIndex(i);           
    //        string jsonStr = JsonConvert.SerializeObject(param);
    //        Debug.Log(jsonStr);

    //        // Json -> Object 변환
    //        Debug.Log(JsonConvert.DeserializeObject<Entity_ItemData>(jsonStr).name);
    //    }
    //}

    public void Init()
    {

    }

    //public Entity_ItemData.Param GetItemDataByIndex(int index)
    //{
    //    Entity_ItemData.Param targetParam = entity_ItemData.sheets[0].list.Find(param => param.index == index);
    //    return targetParam;
    //}

    public void LoadSceneData(Define.Scene type)
    {
        // Managers.Resource.Load<>
    }
}
