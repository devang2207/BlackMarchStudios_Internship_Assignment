using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    //storing the x and z value of the tile location on grid
    public int tileX;
    public int tileZ;

    private Renderer cubeRenderer;//visuals
    private Color defaultColor;

    private void Awake()
    {
        //getting default color at awake and storing for later use in removeHighlight 
        cubeRenderer = GetComponent<Renderer>();
        defaultColor = cubeRenderer.material.color;
    }
    public void Highlight(Color highlightColor)
    {
        //highlight this tile..
        cubeRenderer.material.color = highlightColor;
    }
    public void RemoveHighlight()
    {
        //remove highlight on this tile..
        cubeRenderer.material.color = defaultColor;
    }
}
