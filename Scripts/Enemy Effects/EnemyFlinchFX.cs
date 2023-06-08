using UnityEngine;

public class EnemyFlinchFX : MonoBehaviour
{
    [SerializeField] public GameObject dropDust;
    [SerializeField] public EnemyFlinch enemyFlinch;
    [SerializeField] public Rigidbody2D rb;
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
    public void DisableMovement()
    {
        enemyFlinch.enabled = false;
        enemyFlinch.followPlayer = false;
    }

    public void EnableMovement()
    {
        enemyFlinch.enabled = true;
        enemyFlinch.followPlayer = true;
    }

    public void StaticBodyOn()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void StaticBodyOff()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
