using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    Transform player;

    Vector3 position;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        position = transform.position;
        position.x = player.position.x;
        position.y = player.position.y;
        transform.position = position;
    }
}
