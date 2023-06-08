using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] public GameObject crossfade;
    void Start()
    {
        StartCoroutine(LoadMainMenu());
    }
IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(10);
        crossfade.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
