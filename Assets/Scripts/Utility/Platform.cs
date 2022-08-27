using UnityEngine;

public enum PlatformTypes { Horizontal, Vertical }
public enum PlatformFrequencies { OnceOneWay, Multiple, OnceReversible}

public class Platform : MonoBehaviour
{
    [SerializeField] PlatformTypes platformType;
    [SerializeField] PlatformFrequencies platformFrequency;

    [SerializeField] float travelTime = 1f;
    [SerializeField] float travelSpeed = 1f;

    [SerializeField] bool canReverse = false;

    [SerializeField] bool isActive = false;
    bool isReverse = false;
    float timeCounter = 0;

    private void Update()
    {
        if (isActive)
             MovePlatform();
    }

    public void ExecutePlatform()
    {
        if (!isActive)
            isActive = true;
    }

    [ContextMenu("Execute Plaform")]
    void MovePlatform()
    {
        isActive = true;
        
        if (!isActive)
            return;

            switch (platformFrequency)
            {
                case PlatformFrequencies.OnceOneWay:

                    if (timeCounter < travelTime)
                    {
                        switch (platformType)
                        {
                            case PlatformTypes.Horizontal:
                                    transform.Translate(transform.right * travelSpeed * Time.deltaTime);
                                break;
                            case PlatformTypes.Vertical:
                                transform.Translate(transform.up * travelSpeed * Time.deltaTime);
                                break;
                        }

                        timeCounter += Time.deltaTime;
                    }
                    else
                    {
                        timeCounter = travelTime;
                        isActive = false;
                    }
                    break;

                case PlatformFrequencies.Multiple:

                    if (timeCounter < travelTime)
                    {
                        switch (platformType)
                        {
                            case PlatformTypes.Horizontal:
                                transform.Translate(transform.right * (isReverse ? travelSpeed : -travelSpeed) * Time.deltaTime);
                                break;
                            case PlatformTypes.Vertical:
                                transform.Translate(transform.up * (isReverse ? travelSpeed : -travelSpeed) * Time.deltaTime);
                                break;
                        }
                        timeCounter += Time.deltaTime;
                    }
                    else
                    {
                        timeCounter = 0;
                        isReverse = !isReverse;
                        //isActive = false;
                    }
                    break;
                case PlatformFrequencies.OnceReversible:
                    if (timeCounter < travelTime)
                    {
                        switch (platformType)
                        {
                            case PlatformTypes.Horizontal:
                                transform.Translate(transform.right * (isReverse ? travelSpeed : -travelSpeed) * Time.deltaTime);
                                break;
                            case PlatformTypes.Vertical:
                                transform.Translate(transform.up * (isReverse ? travelSpeed : -travelSpeed) * Time.deltaTime);
                                break;
                        }

                        timeCounter += Time.deltaTime;
                    }
                    else
                    {
                        isReverse = !isReverse;
                        timeCounter = 0;
                        isActive = false;
                    }
                    break;
            }
    
    }
}
