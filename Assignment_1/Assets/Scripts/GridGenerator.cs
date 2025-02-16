using UnityEngine;

public class GridGenerator : MonoBehaviour 
{
    [SerializeField] float spacing = 1.2f;
    [SerializeField] int gridSizeX = 10;
    [SerializeField] int gridSizeZ = 10;

    void Start()
    {
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
                cube.transform.position = position;

                cube.transform.parent = transform;      //keeping all of the cubes created as child of this gameobject.
                cube.AddComponent<TileInfo>();         //adding a Tile info component to every created cube and setting thier positions on x and z
                cube.GetComponent<TileInfo>().tileX = x;
                cube.GetComponent<TileInfo>().tileZ = z;
            }
        }
    }
}
