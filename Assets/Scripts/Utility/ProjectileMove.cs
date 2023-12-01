using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    float _speed;
    float interval;
    public void MoveProjectile(float speed, float time)
    {
        _speed = speed;
        interval = Time.time + time;
        //Destroy(gameObject, time);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        if(Time.time > interval)
            gameObject.SetActive(false);

    }
}
