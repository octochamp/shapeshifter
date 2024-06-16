using UnityEngine;

public class CanonAT1Script : MonoBehaviour
{
    public RenderTexture artworkRenderTexture;
    public Material canvasMaterial;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the user's hand
        if (other.gameObject.CompareTag("Hand"))
        {
            CaptureAndCreateCanvas();
        }
    }

    private void CaptureAndCreateCanvas()
    {
        // Create a new Texture2D from the RenderTexture
        Texture2D tex = new Texture2D(artworkRenderTexture.width, artworkRenderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = artworkRenderTexture;
        tex.ReadPixels(new Rect(0, 0, artworkRenderTexture.width, artworkRenderTexture.height), 0, 0);
        tex.Apply();

        // Create a new material and assign the Texture2D to it
        Material canvasMat = new Material(canvasMaterial);
        canvasMat.mainTexture = tex;

        // Create a new GameObject and add a MeshRenderer and MeshFilter
        GameObject canvas = new GameObject("Canvas");
        canvas.AddComponent<MeshRenderer>().material = canvasMat;
        canvas.AddComponent<MeshFilter>().mesh = CreatePlaneMesh();

        // Set the scale, position, and rotation of the canvas
        canvas.transform.localScale = new Vector3(1.92f, 1.08f, 0.025f);
        canvas.transform.position = new Vector3(0f, 1.8f, 2f);
        canvas.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    }

    private Mesh CreatePlaneMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, 0),
            new Vector3( 0.5f, -0.5f, 0),
            new Vector3( 0.5f,  0.5f, 0),
            new Vector3(-0.5f,  0.5f, 0)
        };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0
        };

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        return mesh;
    }
}