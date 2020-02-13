using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDisplacement_SCR : MonoBehaviour
{
    public float power = 3f;
    public float scale = 1f;
    public float time = 1f;

    private float offsetX;
    private float offsetY;
    private MeshFilter mf;

    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        Noise();
    }

    // Update is called once per frame
    void Update()
    {
        Noise();
        offsetX += Time.deltaTime * time;
        offsetY += Time.deltaTime * time;
    }

    void Noise()
    {
        Vector3[] vertices = mf.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = CalculateHeight(vertices[i].x, vertices[i].z) * power;

            mf.mesh.vertices = vertices;
        }

        float CalculateHeight(float x, float y)
        {
            float xCord = x * scale + offsetX;
            float yCord = y * scale + offsetY;

            return Mathf.PerlinNoise(xCord, yCord);
        }
    }
}
