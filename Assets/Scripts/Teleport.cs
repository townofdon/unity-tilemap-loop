using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] ParticleSystem smokeFX;

    TilemapBounds tilemapBounds;

    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 position;
    bool shouldWarp;

    void Awake()
    {
        tilemapBounds = GameObject.FindWithTag("TilemapBounds").GetComponent<TilemapBounds>();
    }

    void Update()
    {
        position = transform.position;
        shouldWarp = false;
        (minBounds, maxBounds) = tilemapBounds.GetBounds();
        if (transform.position.y > maxBounds.y) WarpVertical(minBounds.y);
        if (transform.position.y < minBounds.y) WarpVertical(maxBounds.y);
        if (transform.position.x > maxBounds.x) WarpHorizontal(minBounds.x);
        if (transform.position.x < minBounds.x) WarpHorizontal(maxBounds.x);
        if (shouldWarp) Warp();
    }

    void WarpVertical(float newY)
    {
        position.y = newY;
        shouldWarp = true;
    }

    void WarpHorizontal(float newX)
    {
        position.x = newX;
        shouldWarp = true;
    }

    void Warp()
    {
        if (smokeFX != null) smokeFX.Pause();
        transform.position = position;
        if (smokeFX != null) smokeFX.Play();
    }
}
