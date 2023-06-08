using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    [SerializeField] public GameObject crossfade;
    void Start()
    {
        StartCoroutine(StartMenu());
    }
    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(2);
        crossfade.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}