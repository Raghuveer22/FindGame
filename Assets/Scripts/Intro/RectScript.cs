using UnityEngine;

[ExecuteInEditMode]
public class RectScript: MonoBehaviour
{
    private RectTransform rectTransform;

    private void OnDrawGizmos()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
            return;

        DrawRectTransformBorders(rectTransform);
    }

    private void DrawRectTransformBorders(RectTransform rectTransform)
    {
        Gizmos.color = Color.red; // You can change the color as per your preference

        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        for (int i = 0; i < 4; i++)
        {
            int nextIndex = (i + 1) % 4;
            Gizmos.DrawLine(corners[i], corners[nextIndex]);
        }
    }
}
