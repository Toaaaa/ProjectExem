using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int armorAmount;//���� �غ� �ܰ迡�� ������ �� ����.

    // Start is called before the first frame update
    void Start()
    {
        armorAmount = GameManager.Instance.inventoryManager.inventoryData.getArmorAmount();//������ scriptable object���� ������ �� ����.
    }
}
