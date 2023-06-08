using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    [SerializeField] public new SpriteRenderer renderer;
    void Update()
    {
        GoSignRegulator();
    }

    public void GoSignRegulator()
    {
        if (Time.fixedTime % .5 < .2)
        {
            renderer.enabled = false;
        }
        else
        {
            renderer.enabled = true;
        }
    }
}
