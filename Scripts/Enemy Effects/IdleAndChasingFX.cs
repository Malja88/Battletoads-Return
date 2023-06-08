using UnityEngine;

public class IdleAndChasingFX : MonoBehaviour
{
    [SerializeField] public GameObject dropDust;
    [SerializeField] public IdleAndChasingAI chasingAI;
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
        chasingAI.enabled = false;
    }

    public void EnableMovement()
    {
        chasingAI.enabled = true;
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
