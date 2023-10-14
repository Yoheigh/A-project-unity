using PlayerOwnedStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameData 
{
    public Difficulty GameDifficulty = Difficulty.Normal;

    public KeyCode KeyBind_Interaction = KeyCode.E;
    public KeyCode KeyBind_Inventory = KeyCode.Tab;
    public KeyCode KeyBind_ = KeyCode.Tab;

    // 플레이어의 상태 저장
    public PlayerStat stat;

    public Dictionary<int, int> ItemDictionary = new Dictionary<int, int>();
    public Dictionary<ItemType, Item> Items = new Dictionary<ItemType, Item>();

    public bool BGMOn = true;
    public bool EffectSoundOn = true;
}
