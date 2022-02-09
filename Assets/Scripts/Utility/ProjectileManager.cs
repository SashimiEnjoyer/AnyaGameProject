using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] float travelSpeed = 1f;

    bool isFacingRight = true;
    bool isActive = false;

    private void Start()
    {

    }
    
    public void SetProjectile(bool _isFacingRIght)
    {
        if(!isActive)
            isActive = true;

        isFacingRight = _isFacingRIght;
    }

}
