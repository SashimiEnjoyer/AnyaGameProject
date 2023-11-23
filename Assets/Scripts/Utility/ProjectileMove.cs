using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    float _speed;
    public void MoveProjectile(float speed, float time)
    {
        _speed = speed;
        Destroy(gameObject, time);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * _speed);
    }
}
