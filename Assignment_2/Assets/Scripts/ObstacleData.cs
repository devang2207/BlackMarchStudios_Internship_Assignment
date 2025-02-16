using UnityEngine;

[System.Serializable]
public class RowData
{
    public bool[] columns = new bool[10]; // Each row contains 10 columns
}

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Grid/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public RowData[] obstacleGrid = new RowData[10]; // 10 rows

    private void OnEnable()
    {
        if (obstacleGrid == null || obstacleGrid.Length != 10)
        {
            obstacleGrid = new RowData[10];
            for (int i = 0; i < 10; i++)
            {
                obstacleGrid[i] = new RowData();
            }
        }
    }
}
