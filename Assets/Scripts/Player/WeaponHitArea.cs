using Cinemachine;
using UnityEngine;

public class WeaponHitArea : MonoBehaviour
{
    [SerializeField] bool isPlayer = true;
    [SerializeField] CinemachineImpulseSource source;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") && isPlayer)
        {
            Debug.Log("hit");
            collision.GetComponent<IEnemy>().EnemyHurted();
            source?.GenerateImpulse();   
        }else if (collision.CompareTag("Player") && !isPlayer)
        {
            collision.GetComponent<PlayerController>().PlayerHurt(transform.position, 1);
        }

    }
}
