using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodbag : Item
{
    // Start is called before the first frame update
    public override void Effort()
    {
        player.GetComponent<Entity>().HPManager.AddMaxHealth(1);
        player.GetComponent<Entity>().HPManager.Heal(1);
    }
}
