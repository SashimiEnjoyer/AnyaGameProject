using UnityEngine;

public class CustomEnemySpawner : MonoBehaviour, IEventSpawner
{
    [SerializeField] private GameObject eventObj;
    [SerializeField] private bool hideInStart;

    private Vector3 eventObjInitialPos;
    private bool isSpawned = false;

    void Awake()
    {
        eventObjInitialPos = eventObj.transform.position;

        if(hideInStart)
            eventObj.SetActive(false);
    }

    public void ExecuteEvent()
    {
        if(isSpawned)
            return;

        eventObj.transform.position = eventObjInitialPos;
        eventObj.SetActive(true);
        eventObj.GetComponent<EnemyController>().ManualStart();
        isSpawned = true;
    }
}
