using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Vector3 move;

    void Start()
    {

    }

    void Update()
    {
        HandleInput();
        Move();
    }

    void HandleInput()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
