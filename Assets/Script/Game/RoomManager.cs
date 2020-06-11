using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Romdon = UnityEngine.Random;
public class RoomManager : MonoBehaviour
{
    public static RoomManager roomManager;
    private MapManager mapManager;

    public GameObject gameStartRoom;
    public GameObject[] monsterRooms;
    public GameObject[] bossRooms;
    public GameObject[] goldRooms;
    public GameObject[] startRooms;
    public GameObject[] doorList;

    private void Awake()
    {
        if (roomManager == null)
            roomManager = this;
        else if (roomManager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        mapManager = GetComponent<MapManager>();
    }

    public int GetDirId(P position)
    {
        for (int i = 0; i < mapManager.dir.Length; ++i)
            if (mapManager.dir[i] == position)
                return i;
        return -1;
    }
    public GameObject GetGameStartRoom()
    {
        return gameStartRoom;
    }
    public GameObject GetMonsterRoom()
    {
        int Id = Romdon.Range(0, monsterRooms.Length);
        GameObject returnRoom = monsterRooms[Id];
        return returnRoom;
    }
    public GameObject GetBoosRoom()
    {
        int Id = Romdon.Range(0, bossRooms.Length);
        GameObject returnRoom = bossRooms[Id];
        return returnRoom;
    }
    public GameObject GetStartRoom()
    {
        int Id = Romdon.Range(0, startRooms.Length);
        GameObject returnRoom = startRooms[Id];
        return returnRoom;
    }

    public GameObject NewRoom(GameObject room, P[] doorDir)
    {
        room = Instantiate(room, mapManager.boardHolder.transform);
        //Door
        for (int i = 0; i < doorDir.Length; ++i)
        {
            if (GetDirId(doorDir[i]) == -1)
                continue;
            string doorName = "" + doorDir[i].x + doorDir[i].y;
            string wallName = "D" + doorDir[i].x + doorDir[i].y;
            Debug.Log(doorName + " " + wallName);
            //room.transform.Find(wallName).gameObject.SetActive(false);
            room.transform.Find(doorName).gameObject.SetActive(true);
        }
        return room;
    }
}
