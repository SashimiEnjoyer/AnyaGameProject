using UnityEngine;

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

    public float playerHealth;
    public Transform currentCheckpoint;

    public bool playerIsDie
    {
        get { return playerHealth <= 0 ? true : false; }
    }

 
}
