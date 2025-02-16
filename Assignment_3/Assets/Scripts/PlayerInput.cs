using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] bool highlightHoveredTile = false;
    [SerializeField] Color highlightColor;

    private GridGenerator gridGenerator;
    private PlayerMovement playerMovement;

    private TileInfo endTile;
    private TileInfo lastHoveredTile = null;

    public bool isMoving = false;
    private void Awake()
    {
        gridGenerator = GridGenerator.Instance;
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Debug.Log(isMoving);
        HighLightHovered();
        MoveToLocation();
    }
    //gets the destination tile and calls move player function
    private void MoveToLocation()
    {
        if (isMoving)//check if the player is moving 
        {  return;  }
        if (Input.GetMouseButtonDown(0))//left mouse click to get the desired location
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                TileInfo tile = hit.collider.GetComponent<TileInfo>();//get the tile which mouse is pointing to
                if (tile != null)
                {
                    endTile = tile;
                    tile.Highlight(Color.red);
                    playerMovement.Move(endTile);
                }
            }
        }
    }

    private void HighLightHovered()
    {
        //casting a ray from camera to screen point
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //highlighting and showing location of tile if found 
            TileInfo currentTile = hit.collider.GetComponent<TileInfo>();
            if (currentTile != null)
            {
                if (lastHoveredTile != null && highlightHoveredTile)
                {
                    lastHoveredTile.RemoveHighlight();
                }
                if (highlightHoveredTile)
                {
                    currentTile.Highlight(highlightColor);
                }
                gridGenerator.ShowLocation(currentTile.tileX, currentTile.tileZ);
                lastHoveredTile = currentTile;
            }
        }
        else if (lastHoveredTile != null )//checks if the player is not hovering on top of a tile
        {
            RemoveHighlight();
        }
    }

    private void RemoveHighlight()  // remove highlight and text
    {
        lastHoveredTile.RemoveHighlight();
        gridGenerator.locationTMP.text = "";
        lastHoveredTile = null;
    }
}
