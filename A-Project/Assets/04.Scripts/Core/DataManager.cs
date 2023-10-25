using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager
{
    public Entity_Dialogue entity_DialogueData;

    public Dictionary<int, Entity_Dialogue.Param> Dialogues;

    public void Init()
    {
        //입력한 액샐값 ScriptableObject로 변환된 데이터를 로딩하기 위해서 
        entity_DialogueData = (Entity_Dialogue)Managers.Resource.Load<ScriptableObject>("ItemData");

        for (int i = 1; i < 5; i++)     //index가 1번 부터 있기 때문
        {
            Entity_Dialogue.Param param = GetDialogueDataByIndex(i);
            string jsonStr = JsonConvert.SerializeObject(param);
            Debug.Log(jsonStr);

            // Json -> Object 변환
            Debug.Log(JsonConvert.DeserializeObject<Entity_Dialogue>(jsonStr).name);
        }
    }

    public Entity_Dialogue.Param GetDialogueDataByIndex(int index)
    {
        Entity_Dialogue.Param targetParam = entity_DialogueData.sheets[0].list.Find(param => param.index == index);
        return targetParam;
    }

    public void LoadSceneData(Define.Scene type)
    {
        // Managers.Resource.Load<>
    }
}
