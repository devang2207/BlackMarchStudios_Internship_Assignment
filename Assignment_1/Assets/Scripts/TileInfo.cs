using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    [SerializeField] private TMPro.TextMeshProUGUI locationTMP;
    private Renderer cubeRenderer;
    private Color defaultColor;
    private void Start()
    {
       cubeRenderer = GetComponent<Renderer>();
       defaultColor = cubeRenderer.material.color;
    }
    public void Highlight(Color highlightColor)
    {
        cubeRenderer.material.color = highlightColor;
    }
    public void RemoveHighlight()
    {
        cubeRenderer.material.color = defaultColor;
    }
}
