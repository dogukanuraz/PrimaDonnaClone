using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject joystick;
    public Image playerDecibel;
    public Slider slider;
    public Gradient gradientBar;
    public Image fill;
    public GameObject losePanel;

    private Player player;




    void Start()
    {
        player = FindObjectOfType<Player>();
        losePanel.SetActive(false);
    }
 
    void Update()
    {        
        playerDecibel.fillAmount = player.CurrentDecibel / player.maxDecibel;
        if (GameManager.LOSE)
        {
            losePanel.SetActive(true);
        }
    }

    public void TryAgainButton()
    {
        GameManager.TRYAGAIN = true;
    }

    public void SetBar(float decibel)
    {
        slider.value = decibel;
        fill.color = gradientBar.Evaluate(slider.normalizedValue);
    }
}
