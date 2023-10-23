using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    const float TILE_SIZE_WIDTH = 64;
    const float TILE_SIZE_HEIGHT = 64;

    [SerializeField]
    ItemSlot[,] itemSlots;

    [SerializeField]
    private ItemSlot selectedItemSlot;

    // private variables

    RectTransform rect;
    Vector2 tileMousePos;
    Vector2Int tileGridPos;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        Init(4, 5);
    }

    private void Init(int width, int height)
    {
        itemSlots = new ItemSlot[width, height];
        rect.sizeDelta = new Vector2(width * TILE_SIZE_WIDTH, height * TILE_SIZE_HEIGHT);
    }

    public Vector2Int GetTileMousePosition(Vector2 mousePosition)
    {
        // 타일 위의 마우스 위치 = 현재 마우스 위치 - rect 트랜스폼의 포지션
        tileMousePos.x = mousePosition.x - rect.position.x;
        tileMousePos.y = rect.position.y - mousePosition.y;

        // 해당 값을 TILE 사이즈로 나눈 다음 int로 변경
        tileGridPos.x = (int)(tileMousePos.x / TILE_SIZE_WIDTH);
        tileGridPos.y = (int)(tileMousePos.y / TILE_SIZE_HEIGHT);

        return tileGridPos;
    }

    public ItemSlot PickUpItem(int posX, int posY)
    {
        ItemSlot item = itemSlots[posX, posY];
        itemSlots[posX, posY] = null;
        return item;
    }

    public void PlaceItem(ItemSlot newItem, int posX, int posY)
    {
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
        rectTransform.SetParent(rect);
        itemSlots[posX, posY] = newItem;

        Vector2 iconPos = new Vector2(posX * TILE_SIZE_WIDTH + TILE_SIZE_WIDTH / 2,
                                    -(posY * TILE_SIZE_HEIGHT + TILE_SIZE_HEIGHT / 2));

        rectTransform.localPosition = iconPos;
    }
}
