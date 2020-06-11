using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomScript : MonoBehaviour
{
    public GameObject HP;
    public GameObject[] item;

    public GameObject get_item()
    {
        return item[Random.Range(0, item.Length)];
    }
    public GameObject get_hp()
    {
        return HP;
    }
}
