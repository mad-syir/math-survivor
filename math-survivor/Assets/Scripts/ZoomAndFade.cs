using UnityEngine;
using UnityEngine.UI;

public class ZoomAndFade : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomSpeed = 1f;
    public float fadeSpeed = 1f;
    public Image blackOverlay;

    private bool isZooming = false;
    private bool isFading = false;
    private float targetSize;
    private Color targetColor;

    private void Start()
    {
        blackOverlay.color = Color.clear;
    }

    private void Update()
    {
        if (isZooming)
        {
            Debug.Log("Zoom");
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);

            if (Mathf.Approximately(mainCamera.orthographicSize, targetSize))
            {
                isZooming = false;
                isFading = true;
            }
        }

        if (isFading)
        {
            blackOverlay.color = Color.Lerp(blackOverlay.color, targetColor, Time.deltaTime * fadeSpeed);

            if (blackOverlay.color.a >= 0.99f)
            {
                // Fading complete, you can trigger the desired action here, such as loading a new scene.
            }
        }
    }

    private void OnMouseDown()
    {
        isZooming = true;
        targetSize = 0.5f;
        targetColor = Color.black;
    }
}
