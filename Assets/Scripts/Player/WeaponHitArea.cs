using UnityEngine;

public class WeaponHitArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            collision.GetComponent<IEnemy>().EnemyHurted();
        }
    }
}
