using FMODUnity;
using UnityEngine;

public class TitleExplosion : MonoBehaviour
{
    [SerializeField] GameObject titleExploison;
    [SerializeField] StudioEventEmitter explosionSFX;
    public void ExplosionOn()
    {
        titleExploison.SetActive(true);
        explosionSFX.Play();
    }

    public void ExplosionOff()
    {
        titleExploison.SetActive(false);
    }
}
