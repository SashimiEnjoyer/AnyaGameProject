using UnityEngine;

public class AdditionalAnimEvent : MonoBehaviour
{
    [SerializeField] private GameObject objToSet;
    public void SetActiveState()
    {
        objToSet.SetActive(true);
    }

    public void SetDeactiveState()
    {
        objToSet.SetActive(false);
    }
}
