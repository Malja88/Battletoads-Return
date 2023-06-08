using FMODUnity;
using System.Collections;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    [Header("Player Prefabs")]
    [SerializeField] public GameObject[] players;
    [Header("Player Spawn Point")]
    [SerializeField] public Transform playerSpawn;
    [Header("Camera Settings")]
    [SerializeField] public CameraHandler cameraHandler;
    [SerializeField] public GameObject[] cameraSpawns;
    [Header("Enemies")]
    [SerializeField] public GameObject[] enemyOnStart;
    [Header("Scene UI")]
    [SerializeField] public GameObject pauseRegulator;
    [SerializeField] public GameObject stageCrossfade;
    [SerializeField] public GameObject characterSelectionScreen;
    [SerializeField] public GameObject stageSign;
    [SerializeField] public GameObject spawnPlayerFX;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter stageSound;
    public void Zitz()
    {
        StartCoroutine(SpawnZitz());
        cameraHandler.enabled = true;
        pauseRegulator.SetActive(true);
        for (int i = 0; i < enemyOnStart.Length; i++)
        {
            enemyOnStart[i].SetActive(true);
        }
        for (int j = 0; j < cameraSpawns.Length; j++)
        {
            cameraSpawns[j].SetActive(true);
        }
        stageCrossfade.SetActive(true);
        stageSign.SetActive(true);
        Invoke("CharacterSelectionScreenOff", 1);
    }
    public void Rash()
    {
        StartCoroutine(SpawnRash());
        cameraHandler.enabled = true;
        pauseRegulator.SetActive(true);
        for (int i = 0; i < enemyOnStart.Length; i++)
        {
            enemyOnStart[i].SetActive(true);
        }
        for (int j = 0; j < cameraSpawns.Length; j++)
        {
            cameraSpawns[j].SetActive(true);
        }
        stageCrossfade.SetActive(true);
        stageSign.SetActive(true);
        Invoke("CharacterSelectionScreenOff", 1);
    }
    public void Pimple()
    {
        StartCoroutine(SpawnPimple());
        cameraHandler.enabled = true;
        pauseRegulator.SetActive(true);
        for (int i = 0; i < enemyOnStart.Length; i++)
        {
            enemyOnStart[i].SetActive(true);
        }
        for (int j = 0; j < cameraSpawns.Length; j++)
        {
            cameraSpawns[j].SetActive(true);
        }
        stageCrossfade.SetActive(true);
        stageSign.SetActive(true);
        Invoke("CharacterSelectionScreenOff", 1);
    }

    public void CharacterSelectionScreenOff()
    {
        characterSelectionScreen.SetActive(false);
    }
  IEnumerator SpawnZitz()
    {
        var zitz = Instantiate(players[0], playerSpawn.position, playerSpawn.rotation);
        if(zitz.TryGetComponent(out PlayerMovement movement))
        {
            spawnPlayerFX.SetActive(true);
            movement.enabled = false;
            yield return new WaitForSeconds(1);
            stageSound.Play();
            yield return new WaitForSeconds(2);
            movement.enabled = true;
            spawnPlayerFX.SetActive(false);
        }
    }

    IEnumerator SpawnRash()
    {
        var zitz = Instantiate(players[1], playerSpawn.position, playerSpawn.rotation);
        if (zitz.TryGetComponent(out PlayerMovement movement))
        {
            spawnPlayerFX.SetActive(true);
            movement.enabled = false;
            yield return new WaitForSeconds(1);
            stageSound.Play();
            yield return new WaitForSeconds(2);
            movement.enabled = true;
            spawnPlayerFX.SetActive(false);
        }
    }

    IEnumerator SpawnPimple()
    {
        var zitz = Instantiate(players[2], playerSpawn.position, playerSpawn.rotation);
        if (zitz.TryGetComponent(out PlayerMovement movement))
        {
            spawnPlayerFX.SetActive(true);
            movement.enabled = false;
            yield return new WaitForSeconds(1);
            stageSound.Play();
            yield return new WaitForSeconds(2);
            movement.enabled = true;
            spawnPlayerFX.SetActive(false);
        }
    }
}
