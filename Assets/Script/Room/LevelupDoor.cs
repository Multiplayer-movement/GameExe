using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelupDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            GameManager.gameManager.LevelUp();
            GameManager.gameManager.player.transform.position = Vector2.zero;
        }
    }
}
