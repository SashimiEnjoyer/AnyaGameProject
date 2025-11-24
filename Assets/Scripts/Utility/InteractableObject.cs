using UnityEngine;
using UnityEngine.Events;

public enum ItemType { Collection, Health, Door, EnemyMassageBox}

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Interactable Items Settings")]
    [SerializeField] private bool isAutoExecute = false;
    [SerializeField] protected bool canRepeat = false;

    [Header("Pop Up Settings")]
    [Tooltip("If Ui PopUp prefab is null, this game object will be floating")]
    [SerializeField] public bool isFloating;
    [SerializeField] bool isHorizontal = false;
    [SerializeField] protected GameObject uiPopUp;

    [SerializeField] float floatingHeight = 0.4f;
    [SerializeField] float floatingFreq = 1f;

    [Header("Custom Event")]
    public UnityEvent onInteracting;

    Vector2 startingPosition;
    Vector2 movingPosition;
    float floatingValue;
    bool interacted = false;

    private void Awake()
    {
        startingPosition = uiPopUp != null? uiPopUp.transform.position : transform.position;
        movingPosition = startingPosition;
    }

    // private void Update()
    // {
    //     if (!isFloating)
    //         return;

    //     //PopUpFloating();
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isAutoExecute)
            {
               ExecuteInteractable();
            }
            else
            {
                if(!interacted)
                    LevelManager.instance.HUDManager.SetInteractPanel(true);
                
                if (uiPopUp != null)
                {
                    uiPopUp.SetActive(true);
                    movingPosition = startingPosition;

                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(isAutoExecute)
                return;
            
            if(!interacted)
                LevelManager.instance.HUDManager.SetInteractPanel(false);
                
            if (uiPopUp != null)
            {
                uiPopUp.SetActive(false);
                floatingValue = 0;
            }
        }
    }

    protected void ExecuteInteractableEvent()
    {
        onInteracting?.Invoke();
    }

    void PopUpFloating()
    {
        floatingValue += Time.deltaTime;
        floatingValue %= (2 * 3.14f);

        if (!isHorizontal)
            movingPosition.y = (Mathf.Sin(floatingValue * floatingFreq) * floatingHeight) + startingPosition.y;
        else
            movingPosition.x = (Mathf.Sin(floatingValue * floatingFreq) * floatingHeight) + startingPosition.x;

        if (uiPopUp != null)
            uiPopUp.transform.position = movingPosition;
        else
            transform.position = movingPosition;
    }

    public void ExecuteInteractable()
    {
        if (canRepeat)
        {
            ExecuteInteractableEvent();
            LevelManager.instance.HUDManager.SetInteractPanel(false);
        }
        else
        {
            if (interacted)
                return;

            ExecuteInteractableEvent();
            LevelManager.instance.HUDManager.SetInteractPanel(false);
            
        }

        interacted = true;
    }
}
