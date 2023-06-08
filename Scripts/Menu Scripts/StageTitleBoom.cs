using UnityEngine;

public class StageTitleBoom : MonoBehaviour
{
    [SerializeField] public float Power;
    [SerializeField] public float Radius;
    [SerializeField] public new Collider2D[] collider;
    [SerializeField] public GameObject stageSign;

    void Update()
    {
        Invoke("Boom", 3);
        if(stageSign != null )
        {
            Destroy(stageSign, 4);
            Destroy(gameObject,4);
        }
    }
        
    void Boom()
    {
        if(collider != null)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                collider[i].isTrigger = true;
            }
        }

        Rigidbody2D[] blocks = FindObjectsOfType<Rigidbody2D>();

        foreach (Rigidbody2D B in blocks)
        {
            if (Vector3.Distance(transform.position, B.transform.position) < Radius)
            {
                Vector3 direction = B.transform.position - transform.position;

                B.AddForce(direction.normalized * Power * (Radius - Vector3.Distance(transform.position, B.transform.position)), ForceMode2D.Impulse);
            }
        }
    }
}
