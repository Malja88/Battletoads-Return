using UnityEngine;

public class CharacterSelectionBackgroundScroll : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;
    public void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0,0);
    }

    void Update()
    {
        Scroll();
    }
}
