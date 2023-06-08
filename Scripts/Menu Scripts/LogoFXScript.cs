using UnityEngine;

public class LogoFXScript : MonoBehaviour
{
    [SerializeField] public GameObject logoDust;
    [SerializeField] public GameObject returnLogo;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logoDust.SetActive(true);
        returnLogo.SetActive(true);
    }
}
