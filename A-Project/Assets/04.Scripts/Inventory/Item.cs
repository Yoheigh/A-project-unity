using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Item
{
    public string key = "";

    public ItemData itemData;

    public Item(string key)
    {
        this.key = key;
    }
}

public class ItemData
{
    public int id;
    public ItemType itemType;
    public string name;
    public string description;
    public Sprite sprite;

    public int itemSizeWidth = 1;
    public int itemSizeHeight = 1;

    public ItemData(int id, string name, ItemType itemType, string description, Sprite sprite)
    {
        this.id = id;
        this.itemType = itemType;
        this.name = name;
        this.description = description;
        this.sprite = sprite;
    }
}