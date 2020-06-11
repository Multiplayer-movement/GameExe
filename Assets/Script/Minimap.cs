using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [Header("小房间父节点")]
    public GameObject minimapRoot;

    [Header("迷你图标")]
    public GameObject minimapRoom;
    public GameObject minimapNullRoom;
    public GameObject minimapBoss;
    public GameObject minimapTreasure;
    public GameObject minimapShop;

    public float width; public float height;
    private void Awake()
    {

    }
    void Start()
    {
        UpdateMinimap();
    }

    public void UpdateMinimap()
    {
        foreach (Transform child in minimapRoot.transform)
        {
            Destroy(child.gameObject);
        }
        P RoomPosition = GameManager.gameManager.playerRoomPosition;
        for (int y = RoomPosition.y + 2; y >= RoomPosition.y - 2; --y)
            for (int x = RoomPosition.x - 2; x <= RoomPosition.x + 2; ++x)
            {
                if (MapManager.mapManager.isVisited(new P(x, y)))
                {
                    var cell = Instantiate(minimapRoom, minimapRoot.transform);
                    if (x == RoomPosition.x && y == RoomPosition.y)
                        cell.GetComponent<Image>().color = new Color(1f, 1f, 1f, 150f / 255f);
                }
                else if(MapManager.mapManager.IsRoom(new P(x, y)))
                {
                    bool flag = true;
                    for(int i=0;i<4 && flag;++i)
                        if (MapManager.mapManager.isVisited(new P(x, y) + MapManager.mapManager.dir[i]))
                        {
                            Instantiate(minimapRoom, minimapRoot.transform);
                            flag = false;
                        }
                    if (flag)
                        Instantiate(minimapNullRoom, minimapRoot.transform);
                }
                else
                {
                    Instantiate(minimapNullRoom, minimapRoot.transform);
                }
            }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (minimapRoot.activeSelf)
            {
                minimapRoot.SetActive(false);
            }
            else
            {
                minimapRoot.SetActive(true);
            }
        }
    }
}
