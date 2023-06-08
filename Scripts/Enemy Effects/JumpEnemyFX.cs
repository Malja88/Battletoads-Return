using UnityEngine;

public class JumpEnemyFX : MonoBehaviour
{
    public Rigidbody2D rb;
    public JumpingEnemy enemyTest;
    public GameObject dropDust;
    public void JumpOn()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void JumpOff()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public void DisableMovement()
    {
        enemyTest.enabled = false;
    }

    public void EnableMovement()
    {
        enemyTest.enabled = true;
    }
    public void DropDustTurnOn()
    {
        dropDust.SetActive(true);
    }

    public void DropDustTurnOff()
    {
        if (dropDust.activeInHierarchy)
        {
            dropDust.SetActive(false);
        }
    }
}
