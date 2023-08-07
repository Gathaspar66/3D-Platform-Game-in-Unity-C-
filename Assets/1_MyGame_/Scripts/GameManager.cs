using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menu1;
    public GameObject menu2;
    public GameObject menu3;
    public GameObject GameOver;
    public GameObject Finish;
    public static GameManager gm;
    public PlayerMovement PlayerMovement;
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI Level1Time;
    public TextMeshProUGUI Level2Time;
    public TextMeshProUGUI Level1Coins;
    public TextMeshProUGUI Level2Coins;
    GameObject mainCameraObject;
    private bool isPaused = false;

    public bool esc = false;


    public TextMeshProUGUI timeText;
    float timeElapsed = 0f;

    private void Start()
    {
        mainCameraObject = GameObject.FindWithTag("GameController");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShowMenu(menu1);
    }

    private void Awake()
    {
        gm = this;
    }

    public void Esc()
    {
        esc = false;
    }

    public void TimeCount(float timeElapsed)
    {
        //timeText.text = "Czas: " + Mathf.Round(timeElapsed) + "s"; // wyœwietla czas na ekranie
        int seconds = Mathf.FloorToInt(timeElapsed);
        int milliseconds = Mathf.FloorToInt((timeElapsed - seconds) * 1000);
        timeText.text = "Czas: " + seconds.ToString("D2") + "." + milliseconds.ToString("D3") + "s";
    }

    void Update()
    {
        LoadTimeToMenu();
        if (Input.GetKeyDown(KeyCode.Escape) && esc)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ShowMenu(menu3);


        isPaused = true;


        Cursor.visible = true;

        PlayerMovement.canMove = false;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        Cursor.visible = false;
        HideMenu(menu3);
        PlayerMovement.canMove = true;
    }


    public void CoinCount(float numberOfCoins)
    {
        CoinsText.text = "Coins: " + numberOfCoins;
    }

    public void LoadLevel(string Level)
    {
        timeElapsed += 0;
        CoinCount(0);
        esc = true;
        LevelManager.lm.LoadLevel(Level);
    }

    public void LoadLevel()
    {
        timeElapsed += 0;
        CoinCount(0);
        esc = true;
        LevelManager.lm.LoadLevel();
    }

    public void HideMenu(GameObject menu)
    {
        menu.transform.position = new Vector3(5000, 0, 0);
    }

    public void ShowMenu(GameObject menu)
    {
        menu.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void UnLoadLevel()
    {
        LevelManager.lm.UnLoadLevel();
        isPaused = false;
    }

    private void LockCursor(bool Lock)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = Lock;
    }

    public void IfPlayerDead()
    {
        ShowMenu(GameOver);
        Esc();
        PlayerMovement.canMove = false;
        LockCursor(true);
    }

    public void FinishGame()
    {
        ShowMenu(Finish);
        Esc();
        PlayerMovement.canMove = false;
        LockCursor(true);
        Time.timeScale = 0f;
    }

    public void MainCamera(bool ifEnabled)
    {
        if (mainCameraObject != null)
        {
            mainCameraObject.SetActive(ifEnabled);
        }
    }

    public void LoadTimeToMenu()
    {
        Level1Time.text = "Time: " + PlayerPrefs.GetFloat("Level1", 0f);
        Level2Time.text = "Time: " + PlayerPrefs.GetFloat("Level2", 0f);

        Level1Coins.text = "Coins: " + PlayerPrefs.GetFloat("Level1Coins", 0f);
        Level2Coins.text = "Coins: " + PlayerPrefs.GetFloat("Level2Coins", 0f);


    }
}