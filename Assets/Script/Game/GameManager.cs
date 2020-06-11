using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public MapManager mapManager;

    public class LEVEL
    {
        public int r;
        public int c;
        public int minNum;
        public int maxNum;
        public LEVEL(int _r, int _c, int _minNum, int _maxNum)
        {
            r = _r;
            c = _c;
            minNum = _minNum;
            maxNum = _maxNum;
        }
        public LEVEL() { }
    }
    LEVEL[] LevelSetting = new LEVEL[10];
    public int Level = 0;

    public P playerRoomPosition;

    public GameObject player;

    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        LevelSetting[0] = new LEVEL(1, 1, 0, 0);
        LevelSetting[1] = new LEVEL(7, 7, 8, 10);

        playerRoomPosition = mapManager.MapBuild(LevelSetting[Level]);
        mapManager.Show();
        MapManager.mapManager.moveRoom(new P(-1, -1), playerRoomPosition);
    }
    public void LevelUp()
    {
        //Level++;
        Level = (Level + 1) % 2;
        playerRoomPosition = mapManager.MapBuild(LevelSetting[Level]);
        mapManager.Show();
        MapManager.mapManager.moveRoom(new P(-1, -1), playerRoomPosition);
    }

}
