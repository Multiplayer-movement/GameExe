using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameManager.gameManager.player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            Invoke("Effort", 0f);
            Destroy(gameObject);
        }
    }
    public virtual void Effort() { }
}