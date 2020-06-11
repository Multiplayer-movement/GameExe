using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class up1 : Item
{
    // Start is called before the first frame update
    public override void Effort()
    {
        player.GetComponent<Entity>().speed *= 1.5f;
        player.GetComponent<Entity>().atk *= 1.5f;
    }
}
