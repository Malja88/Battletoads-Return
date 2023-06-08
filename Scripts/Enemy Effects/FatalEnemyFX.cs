using UnityEngine;

public class FatalEnemyFX : MonoBehaviour
{
    [SerializeField] public PatrolingEnemyScript enemyScript;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public GameObject dropDust;
    public void MovementOff()
    {
        enemyScript.enabled= false;
        enemyScript.followPlayer = false;
    }

    public void MovementOn()
    {
        enemyScript.enabled= true;
    }

    public void StaticBodyOn()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void StaticBodyOff()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void DropDustOn()
    {
        dropDust.SetActive(true);
    }

    public void DropDustOff()
    {
        dropDust.SetActive(false);
    }
}
