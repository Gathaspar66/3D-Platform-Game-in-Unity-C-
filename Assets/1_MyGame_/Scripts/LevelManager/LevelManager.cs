using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager lm;
    public string LoadedLevel;
    public float numberOfCoins = 0;
    public string Level;
    float timeElapsed = 0f;
    private bool startCountTime = false;

    private void Awake()
    {
        lm = this;
    }

    void Update()
    {
        if (startCountTime)
        {
            TimeCount();
        }
    }

    public void LoadLevel(string Level)
    {
        startCountTime = true;
        LoadedLevel = Level;

        SceneManager.LoadSceneAsync(Level, new LoadSceneParameters(LoadSceneMode.Additive));
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        numberOfCoins = 0;
    }

    public void TimeCount()
    {
        timeElapsed += Time.deltaTime;

        GameManager.gm.TimeCount(timeElapsed);
    }

    public void LoadNextLevel()
    {
        int number;
        Match match = Regex.Match(LoadedLevel, @"\d+");
        if (match.Success && int.TryParse(match.Value, out number))
        {
            if (number > 2)
            {
                return;
            }


            number += 1;
            LoadLevel("Level" + number);

            GameManager.gm.esc = true;
        }
        else
        {
            Debug.Log("Nie znaleziono liczby w stringu!");
        }
    }

    public void LoadLevel()
    {
        startCountTime = true;
        LoadLevel(LoadedLevel);
    }

    public void UnLoadLevel()
    {
        timeElapsed = 0;
        startCountTime = false;
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(LoadedLevel);
    }

    public void CoinCount()
    {
        numberOfCoins += 1;
        GameManager.gm.CoinCount(numberOfCoins);
    }

    public void PlayerDie()
    {
        GameManager.gm.IfPlayerDead();
    }

    public void SaveAll()
    {
        if (PlayerPrefs.GetFloat(LoadedLevel, timeElapsed) >= timeElapsed)
        {
            PlayerPrefs.SetFloat(LoadedLevel, timeElapsed);
        }

       
        
        if (PlayerPrefs.GetFloat(LoadedLevel + "Coins", numberOfCoins) <= numberOfCoins)
        {
            PlayerPrefs.SetFloat(LoadedLevel + "Coins", numberOfCoins);

        }
    }
}