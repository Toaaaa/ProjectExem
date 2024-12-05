using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int armorAmount;//사전 준비 단계에서 가져온 방어구 개수.

    // Start is called before the first frame update
    void Start()
    {
        armorAmount = GameManager.Instance.inventoryManager.inventoryData.getArmorAmount();//가방의 scriptable object에서 가져온 방어구 개수.
    }
}
