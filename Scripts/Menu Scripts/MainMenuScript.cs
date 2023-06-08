using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] public GameObject crossfade; 
    public void StartGame()
    {
        StartCoroutine(StartGameCrossfade());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator StartGameCrossfade()
    {
        crossfade.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
