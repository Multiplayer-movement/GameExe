using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    [HideInInspector]
    public float playerAtk;

    public float atk;
    public float force;
    public void Start()
    {
        playerAtk = GameManager.gameManager.player.GetComponent<Entity>().atk;
        Debug.Log(playerAtk);
    }
}
