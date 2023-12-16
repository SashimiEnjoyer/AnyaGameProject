using UnityEngine;

public class WeaponHitArea : MonoBehaviour
{
    [SerializeField] bool isPlayer = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy") && isPlayer)
        {
            Debug.Log("hit");
            collision.GetComponent<IEnemy>().EnemyHurted();

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isPlayer)
        {
            collision.GetComponent<PlayerController>().PlayerHurt(transform.position, 1);
        }

    }
}
