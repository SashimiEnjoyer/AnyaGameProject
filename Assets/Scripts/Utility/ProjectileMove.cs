using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    float _speed;
    float interval;
    float timeCounter = 0f;
    public void MoveProjectile(float speed, float time)
    {
        _speed = speed;
        interval = time;
        timeCounter = 0f;
        //Destroy(gameObject, time);
    }

    private void Update()
    {
        if (LevelManager.instance.GetgameState() != GameplayState.Playing)
            return;

        timeCounter += Time.deltaTime;

        transform.Translate(_speed * Time.deltaTime * Vector2.right);

        if(timeCounter > interval && gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);    
        }
    }

    private void OnDisable()
    {
        Debug.Log("On Disable!!!");
        timeCounter = 0f;
    }
}
