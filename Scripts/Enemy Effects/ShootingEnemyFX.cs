using UnityEngine;

public class ShootingEnemyFX : MonoBehaviour
{
    [SerializeField] Rigidbody2D body2D;
    [SerializeField] public ShootingEnemyAI enemyAI;
 public void StopMoving()
    {
        body2D.velocity= Vector2.zero;
    }

    public void StopShooting()
    {
        enemyAI.shoot = false;
    }

    public void ResumeShooting()
    {
        enemyAI.shoot = true;
    }

    public void DisableMovement()
    {
        enemyAI.enabled= false;
    }

    public void StaticBodyOn()
    {
        body2D.bodyType = RigidbodyType2D.Static;
    }
}
