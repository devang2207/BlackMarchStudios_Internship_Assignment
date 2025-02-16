using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // observer pattern to notify enemy player has moved
    public delegate void PlayerMoved(TileInfo playerTile);
    public static event PlayerMoved OnPlayerMoved;

    [SerializeField] private float moveSpeed = 5f;
    TileInfo currentTile;
    private void Start()
    {
        //setting the current location of player on grid to 0,0 as the prefab spawns there
        currentTile = GridGenerator.Instance.grid[0,0];
    }

    public void Move(TileInfo end)//takes endTile from playerInput
    {
        List<TileInfo> path = AStarPathFinding.FindPath(currentTile, end);
        StartCoroutine(FollowPath(path));
    }
    private IEnumerator FollowPath(List<TileInfo> path)
    {
        GetComponent<PlayerInput>().isMoving = true;
        foreach (TileInfo tile in path) //goes through every tile and continues till the end
        {
            Vector3 targetPosition = new Vector3(tile.transform.position.x, transform.position.y, tile.transform.position.z);
            yield return StartCoroutine(MoveToTile(targetPosition));
        }
        currentTile = path[path.Count - 1];
        OnPlayerMoved?.Invoke(currentTile);
        GetComponent<PlayerInput>().isMoving = false;
    }

    //gets a specified tile location and move towards the tile till its on position
    private IEnumerator MoveToTile(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // Ensures exact positioning
    }

}
