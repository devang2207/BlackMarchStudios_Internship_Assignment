using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    private void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        if (obstacleData == null)
        {
            Debug.LogWarning("ObstacleData is missing!");
            return;
        }

        TileInfo[] allTiles = FindObjectsOfType<TileInfo>();

        foreach (TileInfo tile in allTiles)
        {
            int x = tile.tileX;
            int z = tile.tileZ;

            if (obstacleData.obstacleGrid[x].columns[z]) // Now it reads from the serialized structure
            {
                Vector3 position = tile.transform.position + Vector3.up;
                GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
                obstacle.transform.localScale = Vector3.one * 0.5f;
            }
        }
    }
}
