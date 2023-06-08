using UnityEngine;

public class BossFX : MonoBehaviour
{
    [SerializeField] public GameObject dropDust;
    [SerializeField] public BossAI bossAI;
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
        bossAI.enabled = false;
    }

    public void EnableMovement()
    {
        bossAI.enabled = true;
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
