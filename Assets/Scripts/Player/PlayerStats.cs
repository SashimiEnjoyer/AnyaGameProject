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

    public float currentHealth;
    public Transform currentCheckpoint;

    public bool playerIsDie
    {
        get { return currentHealth <= 0 ? true : false; }
    }

 
}
