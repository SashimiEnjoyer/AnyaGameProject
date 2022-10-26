using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreakableItem : MonoBehaviour, IEnemy
{
    [SerializeField] bool isHorizontal;

    [SerializeField] float floatingHeight = 0.4f;
    [SerializeField] float floatingFreq = 1f;

    Vector2 startingPosition;
    Vector2 movingPosition;
    float floatingValue;
    float timer;
    [SerializeField] bool isShaking;
    [SerializeField] int hitCounter;

    private void Awake()
    {
        startingPosition = transform.position;
        movingPosition = startingPosition;
    }

    private void Update()
    {
        if(!isShaking)
            return;

        if (Time.time < timer)
        {
            Shaking();
        }
        else
            isShaking = false;

    }

    public void EnemyHurted(Vector2 _target)
    {
        timer = Time.time + 0.5f;
        isShaking = true;
        hitCounter++;

        if (hitCounter >= 3)
            Destroy(gameObject);
    }

    void Shaking()
    {
        floatingValue += Time.deltaTime;
        floatingValue %= (2 * 3.14f);

        if (!isHorizontal)
            movingPosition.y = (Mathf.Sin(floatingValue * floatingFreq) * floatingHeight) + startingPosition.y;
        else
            movingPosition.x = (Mathf.Sin(floatingValue * floatingFreq) * floatingHeight) + startingPosition.x;

        transform.position = movingPosition;
        
    }
}
