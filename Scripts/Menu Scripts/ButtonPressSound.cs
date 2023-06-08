using FMODUnity;
using UnityEngine;

public class ButtonPressSound : MonoBehaviour
{
    [SerializeField] StudioEventEmitter buttonSfx;
    public void ButtonPress()
    {
        buttonSfx.Play();
    }
}
