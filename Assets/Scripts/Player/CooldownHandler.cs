using UnityEngine;

public class CooldownHandler : MonoBehaviour
{
    public bool canDash = false;

    [SerializeField] private float dashCooldown;
    float cd;

    [ContextMenu("Use Dash")]
    public void UseDash()
    {
        cd = Time.time + dashCooldown;
    }

    [ContextMenu("Test Cooldown")]
    public bool CanDash()
    {
        return cd <= Time.time;
    }
}
