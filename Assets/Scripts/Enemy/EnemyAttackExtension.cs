using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAttackExtension 
{
    public static RaycastHit2D EnemyTouchPlayer(CapsuleCollider2D boxCollider, LayerMask playerMask,bool _isFacingRight)
    {
        return Physics2D.CapsuleCast(boxCollider.bounds.center, boxCollider.size, CapsuleDirection2D.Horizontal, 0, _isFacingRight ? Vector2.right : Vector2.left, 0.3f, playerMask);
    }
}
