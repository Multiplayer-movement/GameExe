using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : Item
{
    public override void Effort()
    {
        player.GetComponent<Entity>().HPManager.Heal(1);
    }
}
