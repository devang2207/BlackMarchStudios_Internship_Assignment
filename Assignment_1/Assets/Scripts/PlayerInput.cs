using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI locationTMP;
    [SerializeField] Color highlightColor;
    TileInfo lastHoveredTile = null;
    private void Update()
    {
        HighLightSelected();
    }
    private void HighLightSelected()
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
                if (lastHoveredTile != null && lastHoveredTile != currentTile)
                {
                    lastHoveredTile.RemoveHighlight();
                }
                currentTile.Highlight(highlightColor);
                ShowLocation(currentTile.tileX, currentTile.tileZ);
                lastHoveredTile = currentTile;
            }
        }
        else if (lastHoveredTile != null)
        {
            RemoveHighlight();
        }
    }

    private void RemoveHighlight()  // remove highlight and text if player is not hovering on top of a tile
    {
        lastHoveredTile.RemoveHighlight();
        locationTMP.text = "";
        lastHoveredTile = null;
    }

    private void ShowLocation(int tileX,int tileZ)
    {
        locationTMP.text = "TileX:- " + tileX + "\nTileZ:- " + tileZ;
    }
}
