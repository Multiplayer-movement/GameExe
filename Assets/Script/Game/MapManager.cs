using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.UIWidgets.foundation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
public class MapManager : MonoBehaviour
{
    public static MapManager mapManager;
    public RoomManager roomManager;
    public Minimap minimap;

    public GameObject[,] map = new GameObject[1, 1];
    public bool[,] visited;
    public P[] dir = { new P(0, 1), new P(0, -1), new P(-1, 0), new P(1, 0) };
    public int Row;
    public int Column;
    public GameObject boardHolder;

    private void Awake()
    {
        if (mapManager == null)
            mapManager = this;
        else if (mapManager != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        roomManager = GetComponent<RoomManager>();
    }
    private void Start()
    {
        Instantiate(roomManager);
        mapManager = this;
    }
    void InitializeMap()
    {
        map = new GameObject[Row, Column];
        visited = new bool[Row, Column];
    }
    public bool isVisited(P p)
    {
        if (IsRoom(p) == false)
            return false;
        if (visited[p.x, p.y])
            return true;
        return false;
    }
    public bool IsNotRoom(P p)
    {
        if (p.x < 0 || p.x >= Row || p.y < 0 || p.y >= Column)
            return false;
        if (map[p.x, p.y] != null)
            return false;
        return true;
    }
    public bool IsRoom(P p)
    {
        if (p.x < 0 || p.x >= Row || p.y < 0 || p.y >= Column)
            return false;
        if (map[p.x, p.y] == null)
            return false;
        return true;
    }
    int AllDirRoomNum(P p)
    {
        int res = 0;
        for (int i = 0; i < 4; ++i)
            res += IsNotRoom(p + dir[i]) ? 1 : 0;
        return res;
    }

    //建地图
    public P MapBuild(GameManager.LEVEL Level)
    {
        for (int i = 0; i < map.GetLength(0); ++i)
            for (int j = 0; j < map.GetLength(1); ++j)
                if (map[i, j] != null)
                    Destroy(map[i, j]);
        Row = Level.r;
        Column = Level.c;
        InitializeMap();
        int roomNum = Random.Range(Level.minNum, Level.maxNum + 1);
        P StartRoomPosition = new P(Row / 2, Column / 2);
        Debug.Log(roomNum);
        if (roomNum == 0)
            map[StartRoomPosition.x, StartRoomPosition.y] = roomManager.GetGameStartRoom();
        else
            map[StartRoomPosition.x, StartRoomPosition.y] = roomManager.GetStartRoom();
        Queue<P> q = new Queue<P>();
        for (int i = 0; i < 4; ++i)
            q.Enqueue(StartRoomPosition);
        while (roomNum > 0)
        {
            while (q.isNotEmpty() && roomNum > 0)
            {
                P p = q.Dequeue();
                int dirId = Random.Range(0, 4);
                bool[] num = { false, false, false, false };
                bool noSpace = false;
                while (!IsNotRoom(p + dir[dirId]) && !noSpace)
                {
                    dirId = Random.Range(0, 4);
                    num[dirId] = true;
                    if (num[0] && num[1] && num[2] && num[3])
                        noSpace = true;
                }
                if (noSpace)
                    break;
                p = p + dir[dirId];
                GameObject room;
                if (roomNum > 1)
                    room = roomManager.GetMonsterRoom();
                else
                    room = roomManager.GetBoosRoom();
                roomNum--;
                map[p.x, p.y] = room;
                q.Enqueue(p);
            }
            while (q.isEmpty())
            {
                P p = new P(Random.Range(0, Row + 1), Random.Range(0, Column + 1));
                if (IsNotRoom(p) == true && AllDirRoomNum(p) < 4)
                    q.Enqueue(p);
            }
        }
        return StartRoomPosition;
    }

    //进入房间或者离开房间
    public void SetRoomActiveTrue(P mapPosition)
    {
        if (isVisited(mapPosition))
        {
            map[mapPosition.x, mapPosition.y].SetActive(true);
        }
        else
        {
            P[] roomDir = { new P(), new P(), new P(), new P() };
            for (int i = 0; i < 4; ++i)
                if (IsRoom(mapPosition + dir[i]))
                    roomDir[i] = dir[i];
            visited[mapPosition.x, mapPosition.y] = true;
            map[mapPosition.x, mapPosition.y] = roomManager.NewRoom(map[mapPosition.x, mapPosition.y], roomDir);
        }
        GameManager.gameManager.playerRoomPosition = mapPosition;
    }
    private void SetRoomActiveFalse(P position)
    {
        map[position.x, position.y].SetActive(false);
    }

    public void moveRoom(P preRoomPosition, P nextRoomPosition)
    {
        if (preRoomPosition.x != -1 && preRoomPosition.y != -1)
            SetRoomActiveFalse(preRoomPosition);
        SetRoomActiveTrue(nextRoomPosition);
        minimap.UpdateMinimap();
    }


    public void Show()
    {
        StringBuilder str = new StringBuilder();
        for (int y = Row - 1; y >= 0; --y)
        {
            for (int x = 0; x < Column; ++x)
            {
                if (map[x, y] != null)
                    str.Append("1");
                else
                    str.Append("0");
            }
            str.Append("\n");
        }
        print(str);
    }
}
