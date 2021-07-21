using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private FloatingJoystick floatingJoystick;
    private AnimationManager animationManager;
    private LevelManager levelManager;
    private Rigidbody playerRB;

    public GameObject finalScene;

    public float tapCount;


    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        floatingJoystick = FindObjectOfType<FloatingJoystick>();
        animationManager = FindObjectOfType<AnimationManager>();
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (levelManager.levelState)
        {
            case LevelState.Run:
                RunControl();
                break;
            case LevelState.Tap:
                TapControl();
                break;
            case LevelState.Sign:
                SignControl();
                break;
            default:
                break;
        }

    }
    
    void RunControl()
    {
        playerRB.velocity = new Vector3(floatingJoystick.Horizontal * moveSpeed, playerRB.velocity.y, floatingJoystick.Vertical * moveSpeed);
        transform.LookAt(-new Vector3(floatingJoystick.Horizontal * moveSpeed, playerRB.velocity.y, floatingJoystick.Vertical * moveSpeed) * 10);

        if (floatingJoystick.Dragging)
        {
            animationManager.PlayerRunAnim();
        }
        else if (floatingJoystick.Dragging == false)
        {
            animationManager.PlayerIdleAnim();
        }
    }

    void TapControl()
    {
        animationManager.PodiumWalk();
        transform.LookAt(finalScene.transform);
        transform.position = Vector3.MoveTowards(transform.position,
            finalScene.transform.position, 1f* Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            tapCount += 50;
            Debug.Log(tapCount);
        }

        if (tapCount > 10)
        {
            tapCount -= 10 * Time.deltaTime;
        }
    }

    void SignControl()
    {
        transform.position = finalScene.transform.position;
        animationManager.Charge();
    }

}
