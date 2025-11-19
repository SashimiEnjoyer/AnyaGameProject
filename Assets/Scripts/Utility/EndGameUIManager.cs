using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour
{
    
    public void SetActiveState(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
