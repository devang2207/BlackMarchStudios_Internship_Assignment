using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour , IEnemyAI
{
    [SerializeField] private float moveSpeed = 5f;
    bool isMoving = false;
    TileInfo currentTile;

    private void Start()
    {
        //setting last tile of grid representing enemy current location
        currentTile = GridGenerator.Instance.grid[9, 9];
    }
    public void FollowPlayer(TileInfo playerTile)
    {
        if (isMoving)
        {
            return;
        }
        List<TileInfo> path = AStarPathFinding.FindPath(currentTile, playerTile);
        StartCoroutine(FollowPath(path));
    }
    private IEnumerator FollowPath(List<TileInfo> path)
    {
        isMoving = true;
        //break if the path is less then 2 tiles 
        if (path.Count < 2)
        {
            yield break;
        }
        for (int i = 0; i < path.Count-1; i++)//stopping 1 tile before reaching player..
        {
            Vector3 targetPosition = new Vector3(path[i].transform.position.x, transform.position.y, path[i].transform.position.z);
            yield return StartCoroutine(MoveToTile(targetPosition));
        }
        isMoving = false;
        currentTile = path[path.Count - 2];
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

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMoved += FollowPlayer;//subscribe to static event in playermovement
    }
    private void OnDisable()
    {
        PlayerMovement.OnPlayerMoved += FollowPlayer;//unsubscribe to static event in playermovement
    }
}
