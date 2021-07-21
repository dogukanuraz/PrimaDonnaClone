using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public float maxDecibel;
    [SerializeField] private float currentDecibel;

    private LevelManager levelManager;
    private AnimationManager animationManager;

    public GameObject screamFX;
    public GameObject finalFX;
    public GameObject _ENEMIES;
    List<GameObject> enemies = new List<GameObject>();

    private AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip finalSound;

    public float CurrentDecibel
    {
        get => currentDecibel;
        set {
            currentDecibel = value;
            if (currentDecibel <=0)
            {
                currentDecibel = 0;
            }
            else if (currentDecibel >= maxDecibel)
            {
                currentDecibel = maxDecibel;
            }

        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animationManager = FindObjectOfType<AnimationManager>();
        levelManager = FindObjectOfType<LevelManager>();
        CurrentDecibel = 0;
    }

    private void Update()
    {
        CurrentDecibel += Time.deltaTime;



        if (EnemyClose())
        {
            AttackToEnemy();

        }



    }

    private bool EnemyClose()
    {
        if (enemies.Any())
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void AttackToEnemy()
    {


        foreach (GameObject enemy in enemies)
        {
            if (CurrentDecibel > 10)
            {
                enemy.GetComponentInParent<Enemy>().currentState = EnemyState.Die;
                CurrentDecibel -= 10;
                animationManager.PlayerAttack();
                screamFX.SetActive(true);
                audioSource.PlayOneShot(attackSound);
            }
        }
        enemies.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {

            enemies.Add(other.gameObject);
        }

        if (other.tag == "Collectable")
        {
            CurrentDecibel += 25;
            Destroy(other.gameObject);
        }

        if (other.tag == "FinishRun")
        {
            levelManager.levelState = LevelState.Tap;
            Destroy(_ENEMIES.gameObject);

        }

        if (other.tag == "Finish")
        {
            levelManager.levelState = LevelState.Sign;
            audioSource.PlayOneShot(finalSound);
            finalFX.SetActive(true);

            StartCoroutine(WinGame());
        }
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1f);
        GameManager.WIN = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }

}
