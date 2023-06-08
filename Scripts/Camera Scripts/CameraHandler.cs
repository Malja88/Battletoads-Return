using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    /// <summary>
    /// Follow player camera Script
    /// </summary>
    [SerializeField] public Transform camTransform;
    [SerializeField] public Transform player;
    [SerializeField] public float rightCameraBorder;
    [SerializeField] public float leftCameraBorder;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        camTransform.position = new Vector3(player.position.x, camTransform.position.y, camTransform.position.z);
        camTransform.position = new Vector3(Mathf.Clamp(camTransform.position.x, leftCameraBorder, rightCameraBorder), 
            camTransform.transform.position.y, camTransform.transform.position.z);
    }
}
