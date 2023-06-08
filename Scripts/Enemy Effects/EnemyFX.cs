using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] public GameObject dropDust;
    [SerializeField] public FollowPlayer followPlayer;
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
        followPlayer.enabled= false;
    }

    public void EnableMovement()
    {
        followPlayer.enabled= true;
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
