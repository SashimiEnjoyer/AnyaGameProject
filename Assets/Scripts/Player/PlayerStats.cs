using UnityEngine;

[System.Serializable]
public struct Stats
{
    public float healthMax;
    public float gravityForce;
}

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public static string playerName;

    [SerializeField] private HUDManager HUDManager;
    private float health;
 
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Debug.Log("PLayer Name is : " + playerName);
    }

    private void Start()
    {
        currentHealth = startingStats.healthMax;
    }

    public Stats startingStats;

    public float currentHealth
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0, startingStats.healthMax);
            HUDManager.SetHealthBar(health);
        }
            
    }

    public Transform currentCheckpoint;

    public bool playerIsDie
    {
        get { return currentHealth <= 0 ? true : false; }
    }

 
}
