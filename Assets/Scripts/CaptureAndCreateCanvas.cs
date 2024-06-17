using UnityEngine;

public class CaptureScript : MonoBehaviour
{
    public RenderTexture artworkRenderTexture;
    public Material canvasMaterial;
    public Material capturedImageMat;
    private void CaptureAndCreateCanvas()
    {
        Debug.Log("Starting CaptureAndCreateCanvas");

        // Check if the RenderTexture is set and active
        if (artworkRenderTexture == null)
        {
            Debug.LogError("artworkRenderTexture is null");
            return;
        }

        // Create a new Texture2D from the RenderTexture
        Texture2D tex = new Texture2D(artworkRenderTexture.width, artworkRenderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = artworkRenderTexture;
        tex.ReadPixels(new Rect(0, 0, artworkRenderTexture.width, artworkRenderTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null; // Reset the RenderTexture

        Debug.Log("Texture2D created from RenderTexture");

        // Check if the canvasMaterial is set
        if (capturedImageMat == null)
        {
            Debug.LogError("capturedImageMat is null");
            return;
        }

        capturedImageMat.mainTexture = tex;

        Debug.Log("Base Map of capturedImageMat replaced with new Texture2D");
    }

}
