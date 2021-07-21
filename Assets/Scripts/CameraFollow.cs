using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        switch (levelManager.levelState)
        {
            case LevelState.Run:
                transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
                break;
            case LevelState.Tap:
                transform.position = new Vector3(player.transform.position.x + 10, player.transform.position.y + 4, player.transform.position.z);
                transform.LookAt(player.transform);
                break;
            case LevelState.Sign:
                transform.position = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
                transform.LookAt(player.transform);
                break;
            default:
                break;
        }        
    }
}
