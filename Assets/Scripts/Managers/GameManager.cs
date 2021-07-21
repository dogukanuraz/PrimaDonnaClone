using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool WIN;
    public static bool LOSE;
    public static bool TRYAGAIN;

    private void Start()
    {
        Time.timeScale = 1;
        WIN = false;
        LOSE = false;
        TRYAGAIN = false;
    }

    private void Update()
    {
        if (TRYAGAIN)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (WIN)
        {
            int activeScene = SceneManager.GetActiveScene().buildIndex;

            if (activeScene ==0)
            {
                SceneManager.LoadScene(1);
            }
            else if (activeScene == 1)
            {
                SceneManager.LoadScene(0);
            }

        }

        if (LOSE)
        {
            Time.timeScale = 0;
        }
    }
}
