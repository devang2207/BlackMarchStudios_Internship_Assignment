using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridGenerator : MonoBehaviour 
{
    [SerializeField] GameObject playerPrefab;                   //player prefab to be spawned..
    [SerializeField] public TMPro.TextMeshProUGUI locationTMP;  //tile location indicator text

    [SerializeField] private float spacing = 1.2f;
    [SerializeField] public int gridSizeX = 10;
    [SerializeField] public int gridSizeZ = 10;

    public TileInfo[,] grid;
    public static GridGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        grid = new TileInfo[gridSizeX, gridSizeZ];
        CreateGrid();
    }
    void CreateGrid()
    { 
        //going through x and z values for each adding 1 cube
        for (int x = 0; x < gridSizeX; x++) 
        { 
            for (int z = 0; z < gridSizeZ; z++) 
            { 
                //creating cubes
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //setting positions for the cubes on grid
                Vector3 position = new Vector3(x * spacing, 0, z * spacing); 

                if(x == 0 && z == 0)//spawning player on 0,0 tile..
                {
                    Vector3 spawnPosition = new Vector3(position.x, position.y + 1, position.z);
                    Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
                }
                cube.transform.position = position;

                cube.transform.parent = transform;      //keeping all of the cubes created as child of this gameobject.
                
                TileInfo tileInfo = cube.AddComponent<TileInfo>();         //adding a Tile info component to every created cube and setting thier positions on x and z
                cube.GetComponent<TileInfo>().tileX = x;
                cube.GetComponent<TileInfo>().tileZ = z;
                grid[x,z] = tileInfo;
            }
        }
    }
    public void ShowLocation(int tileX, int tileZ)
    {
        locationTMP.text = "TileX:- " + tileX + "\nTileZ:- " + tileZ;
    }
}
