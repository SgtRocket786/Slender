using UnityEngine;

[ExecuteInEditMode]
public class RoadGenerator : MonoBehaviour
{
    public Vector3 startPoint = Vector3.zero; // Start position of the road
    public Vector3 direction = new Vector3(0, 0, 1); // Direction of the road
    public float roadWidth = 2f; // Width of the road
    public float roadLength = 20f; // Length of the road
    public Material roadMaterial; // Material for the road

    void OnValidate()
    {
        GenerateStraightRoadMesh();
    }

    void GenerateStraightRoadMesh()
    {
        Mesh roadMesh = new Mesh();

        // Define vertices
        Vector3[] vertices = new Vector3[4];
        Vector3 right = Vector3.Cross(Vector3.up, direction.normalized) * roadWidth * 0.5f;

        vertices[0] = startPoint - right; // Bottom-left
        vertices[1] = startPoint + right; // Bottom-right
        vertices[2] = startPoint + direction.normalized * roadLength - right; // Top-left
        vertices[3] = startPoint + direction.normalized * roadLength + right; // Top-right

        // Define triangles
        int[] triangles = new int[]
        {
            0, 2, 1, // First triangle
            1, 2, 3  // Second triangle
        };

        // Define UVs for texture mapping
        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0, 1);
        uvs[3] = new Vector2(1, 1);

        // Assign to mesh
        roadMesh.vertices = vertices;
        roadMesh.triangles = triangles;
        roadMesh.uv = uvs;
        roadMesh.RecalculateNormals();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = roadMesh;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null) meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = roadMaterial;
    }
}
