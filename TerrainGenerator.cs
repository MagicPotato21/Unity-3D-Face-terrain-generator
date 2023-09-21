using UnityEngine;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject topCubePrefab;    // Drag your top cube prefab (the one for the top layers) here in the Unity Editor
    public GameObject bottomCubePrefab; // Drag your bottom cube prefab (the one for the bottom layer) here in the Unity Editor
    public GameObject specialCubePrefab; // Drag your special cube prefab here in the Unity Editor
    public int terrainWidth = 100;      // Width of the terrain in cubes
    public int terrainHeight = 100;     // Height of the terrain in cubes
    public float cubeSize = 1.0f;       // Size of each cube
    public float bottomLayerHeight = 0.0f; // Height at which to place the bottom layer cubes
    public int minSpecialCubeCount = 10;  // Minimum number of special cubes
    public int maxSpecialCubeCount = 20;  // Maximum number of special cubes

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        int specialCubeCount = Random.Range(minSpecialCubeCount, maxSpecialCubeCount + 1); // Randomly determine the number of special cubes

        List<Vector2> specialCubePositions = new List<Vector2>();

        for (int i = 0; i < specialCubeCount; i++)
        {
            int x = Random.Range(0, terrainWidth);
            int y = Random.Range(1, terrainHeight); // Start from 1 to exclude the bottom layer
            specialCubePositions.Add(new Vector2(x, y));
        }

        for (int x = 0; x < terrainWidth; x++)
        {
            for (int y = 0; y < terrainHeight; y++)
            {
                float xPos = x * cubeSize;
                float yPos = y * cubeSize;

                // Determine if this cube should be a bottom layer cube, a top layer cube, or a special cube
                GameObject cubePrefab;
                if (y == 0)
                {
                    cubePrefab = bottomCubePrefab;
                }
                else if (specialCubePositions.Contains(new Vector2(x, y)))
                {
                    cubePrefab = specialCubePrefab;
                }
                else
                {
                    cubePrefab = topCubePrefab;
                }

                // Create a cube at the specified position
                GameObject cube = Instantiate(cubePrefab, new Vector3(xPos, yPos, bottomLayerHeight), Quaternion.identity);
                cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            }
        }
    }
}
