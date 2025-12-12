using UnityEngine;

public class WeaponHitArea : MonoBehaviour
{
    [SerializeField] bool isPlayer = true;


    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isPlayer)
        {
            Debug.Log("hit");
            collision.GetComponent<IEnemy>().EnemyHurted();

        }

        if (collision.CompareTag("Player") && !isPlayer)
        {
            collision.GetComponent<PlayerController>().PlayerHurt(transform.position, 1);
        }

    }
}
