using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public GameObject Camera;

    void Start()
    {
        Camera.transform.rotation = transform.rotation;
        Camera.transform.position = new Vector3(transform.position.x, transform.position.y + 1.33f, transform.position.z - 0.61f);
    }

    void Update()
    {
    }
}
