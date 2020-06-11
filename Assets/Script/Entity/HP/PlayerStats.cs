using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    public float health;
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    private float maxHealthLimited;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxHealthLimited { get { return maxHealthLimited; } }

    public void SetHealth(float health, float maxHealth, float maxHealthLimited)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.maxHealthLimited = maxHealthLimited;
        ClampHealth();
    }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        ClampHealth();
    }

    public void AddMaxHealth(int num)
    {
        if (maxHealth < maxHealthLimited)
        {
            maxHealth = Mathf.Min(maxHealth + num, maxHealthLimited);
            health = Mathf.Min(health + num, maxHealthLimited);

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
}
