using UnityEngine;

public class PatrolingWithRandomAttacksFX : MonoBehaviour
{
    [SerializeField] public GameObject dropDust;
    [SerializeField] public PatrolingScriptWithRandomAttacks randomAttacks;
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
        randomAttacks.enabled = false;
    }

    public void EnableMovement()
    {
        randomAttacks.enabled = true;
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
