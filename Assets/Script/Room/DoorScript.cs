using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int doorDirX;
    public int doorDirY;
    public Vector2 doorPosition;
    public Vector2 playerPositionInRoom;
    private void OnTriggerEnter2D(Collider2D EntityCollider2D)
    {
        if (EntityCollider2D.CompareTag("Player"))
        {
            Transform monsterList = transform.parent.Find("MonsterList");
            if (monsterList.childCount > 0)
                return;
            P doorDir = new P(doorDirX, doorDirY);
            Debug.Log("" + doorDirX + " " + doorDirY);
            //move room
            MapManager.mapManager.moveRoom(GameManager.gameManager.playerRoomPosition, doorDir + GameManager.gameManager.playerRoomPosition);
            //move playerPossition
            GameObject nextRoom = MapManager.mapManager.map[GameManager.gameManager.playerRoomPosition.x, GameManager.gameManager.playerRoomPosition.y];

            P doorDir2 = new P() - doorDir;
            var doorName = "" + doorDir2.x + doorDir2.y;
            Transform nowDoor = nextRoom.transform.Find(doorName);
            CompositeCollider2D compositeCollider2D = nowDoor.GetComponent<CompositeCollider2D>();
            Vector3 newPosition = Vector3.zero;
            newPosition.x = compositeCollider2D.bounds.center.x + doorDir.x * compositeCollider2D.bounds.size.x * 1.5f;
            newPosition.y = compositeCollider2D.bounds.center.y + doorDir.y * compositeCollider2D.bounds.size.y * 1.5f;
            GameManager.gameManager.player.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
        }
    }
}