using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapBounds : MonoBehaviour
{
    Tilemap tilemap;
    Bounds bounds;

    bool didInit;

    public (Vector2 min, Vector2 max) GetBounds()
    {
        if (!didInit) throw new UnityException("GetBounds was called before init finished");
        return (
            transform.position + bounds.center - bounds.extents,
            transform.position + bounds.center + bounds.extents
        );
    }

    public Vector2 GetSize()
    {
        if (!didInit) throw new UnityException("GetSize was called before init finished");
        return bounds.size;
    }

    public Vector2 GetCenter()
    {
        if (!didInit) throw new UnityException("GetSize was called before init finished");
        return transform.position + bounds.center;
    }

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        bounds = tilemap.localBounds;
        didInit = true;
    }

    void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        if (tilemap == null) return;

        Gizmos.color = Color.green;
        Vector2 offset = transform.position + bounds.center;
        Vector2 TL = offset + new Vector2(-bounds.extents.x, bounds.extents.y);
        Vector2 TR = offset + new Vector2(bounds.extents.x, bounds.extents.y);
        Vector2 BR = offset + new Vector2(bounds.extents.x, -bounds.extents.y);
        Vector2 BL = offset + new Vector2(-bounds.extents.x, -bounds.extents.y);
        Gizmos.DrawLine(TL, TR);
        Gizmos.DrawLine(TR, BR);
        Gizmos.DrawLine(BL, BR);
        Gizmos.DrawLine(TL, BL);
    }
}
