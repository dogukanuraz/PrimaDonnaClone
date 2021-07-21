using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public enum LevelState
{
    Run,
    Tap,
    Sign
}

public class LevelManager : MonoBehaviour
{
    public LevelState levelState;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerController playerController;

    

    private void Update()
    {
        uiManager.SetBar(playerController.tapCount);

        switch (levelState)
        {
            case LevelState.Run:
                uiManager.slider.gameObject.SetActive(false);
                

                break;
            case LevelState.Tap:
                uiManager.joystick.SetActive(false);
                uiManager.playerDecibel.enabled = false;
                uiManager.slider.gameObject.SetActive(true);
                break;
            case LevelState.Sign:
                uiManager.slider.gameObject.SetActive(false);

                break;
            default:
                break;
        }
    }

}
