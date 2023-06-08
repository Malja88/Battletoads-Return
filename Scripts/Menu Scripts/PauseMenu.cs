using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject[] pauseAnimations;
    [SerializeField] public GameObject crossfade;
    [SerializeField] public StudioEventEmitter stageSound;
    public static bool GameIsPaused = false;
    void Update()
    {
        PauseGame();
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        for (int i = 0; i < pauseAnimations.Length; i++)
        {
            pauseAnimations[i].SetActive(false);
        }
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseAnimations[Random.Range(0,3)].SetActive(true);
    }
    public void LoadMenu()
    {
        StartCoroutine(Crossfade());
    }
    public void RestartRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    IEnumerator Crossfade()
    {
        Time.timeScale = 1;
        crossfade.SetActive(true);
        yield return new WaitForSeconds(2);
        stageSound.Stop();
        SceneManager.LoadScene(1);
    }
}