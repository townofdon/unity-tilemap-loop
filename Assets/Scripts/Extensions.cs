using UnityEngine;

public static class Extensions
{
    public static Vector2 heading(this Rigidbody2D rb)
    {
        return rb.velocity.normalized;
    }

    public static Color toAlpha(this Color color, float alpha)
    {
        Color temp = color;
        temp.a = Mathf.Clamp01(alpha);
        return temp;
    }

    public static void SetActiveAndEnable(this MonoBehaviour component, bool isActive)
    {
        component.enabled = isActive;
        component.gameObject.SetActive(isActive);
    }

    public static void SetActiveAndEnable(this Renderer renderer, bool isActive)
    {
        renderer.enabled = isActive;
        renderer.gameObject.SetActive(isActive);
    }
}
