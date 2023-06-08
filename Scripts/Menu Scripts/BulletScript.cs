using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] public new Rigidbody2D rigidbody;
    [SerializeField] public Transform player;
    [SerializeField] public new SpriteRenderer renderer;
    [SerializeField] public float speed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player.position.x < transform.position.x)
        {
            renderer.flipX= true;
            rigidbody.velocity = transform.right * speed;
        }
        if (player.position.x > transform.position.x)
        {
            rigidbody.velocity = transform.right * -speed;
            renderer.flipX = false;
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}
