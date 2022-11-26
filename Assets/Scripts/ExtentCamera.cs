using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ExtentCamera : MonoBehaviour
{
    enum Orientation
    {
        Vertical,
        Horizontal,
        Corner,
    }

    [SerializeField] Orientation orientation;
    [SerializeField] Color debugColor = Color.cyan.toAlpha(0.5f);

    TilemapBounds tilemapBounds;
    Transform mainCameraTransform;
    Camera mainCamera;
    Camera currentCamera;
    Vector2 tilemapCenter;
    Vector2 tilemapSize;
    Vector3 offsetFromMainCamera;

    Mesh debugMesh;

    void Awake()
    {
        tilemapBounds = GameObject.FindWithTag("TilemapBounds").GetComponent<TilemapBounds>();
        currentCamera = GetComponent<Camera>();
        mainCamera = transform.parent?.GetComponent<Camera>();
        if (mainCamera == null) throw new UnityException("ExtentCamera must be a child of the MainCamera");
        mainCameraTransform = mainCamera.transform;
    }

    void Start()
    {
        MatchMainCameraSettings();
        tilemapSize = tilemapBounds.GetSize();
        tilemapCenter = tilemapBounds.GetCenter();
    }

    void Update()
    {
        offsetFromMainCamera = tilemapSize;
        if (orientation == Orientation.Vertical) offsetFromMainCamera.x = 0;
        if (orientation == Orientation.Horizontal) offsetFromMainCamera.y = 0;
        offsetFromMainCamera.z = transform.localPosition.z;
        if (mainCameraTransform.position.x > tilemapCenter.x) offsetFromMainCamera.x *= -1;
        if (mainCameraTransform.position.y > tilemapCenter.y) offsetFromMainCamera.y *= -1;
        transform.localPosition = offsetFromMainCamera;
    }

    void MatchMainCameraSettings()
    {
        currentCamera.orthographic = true;
        currentCamera.orthographicSize = mainCamera.orthographicSize;
        currentCamera.clearFlags = CameraClearFlags.Nothing;
    }

    void GenerateDebugMesh()
    {
        Vector2 extents = currentCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height) * 0.5f) - transform.position;
        Vector3 TL = new Vector2(-extents.x, extents.y);
        Vector3 TR = new Vector2(extents.x, extents.y);
        Vector3 BL = new Vector2(-extents.x, -extents.y);
        Vector3 BR = new Vector2(extents.x, -extents.y);

        debugMesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        vertices[0] = TL;
        vertices[1] = TR;
        vertices[2] = BL;
        vertices[3] = BR;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        debugMesh.vertices = vertices;
        debugMesh.triangles = triangles;
        debugMesh.RecalculateNormals();
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (currentCamera == null) return;
        if (debugMesh == null) GenerateDebugMesh();
        Gizmos.color = debugColor;
        Gizmos.DrawMesh(debugMesh, -1, transform.position, Quaternion.identity, Vector3.one);
    }
}
