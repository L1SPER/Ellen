using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    int mainScore = 0;
    int highestScore = 0;
    public int trophyCounter = 0;
    private TextMeshProUGUI trophyText;
    private TextMeshProUGUI mainScoreText;
    private TextMeshProUGUI menuScoreText;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject player;
    public Vector3 startPosition;
    private static GameManager instance;
    public static bool gameIsPaused;
    public List<Vector3> collectedCoins = new List<Vector3>();
    public List<Vector3> collectedKeys = new List<Vector3>();
    public GameObject Coin;
    public GameObject Key;


    private void Awake()
    {
        Singleton();
        mainScoreText = GameObject.FindWithTag("MainScore").GetComponent<TextMeshProUGUI>();
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        winMenu = GameObject.FindWithTag("WinMenu");
        player = GameObject.FindWithTag("Player");
    }
    private void Singleton()
    {
        if(instance!=null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        mainScoreText.text = 0.ToString();
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        trophyCounter = 0;
        mainScore = 0;
        startPosition = player.transform.position;
    }
    private void Update()
    {
        mainScoreText.text = mainScore.ToString();
        PauseMenuControl();
        Debug.Log("StartPos: " + startPosition);
    }
    public void PauseMenuControl()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioListener.volume = 1f;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused=false;
    }
    public void Pause()
    {
        AudioListener.volume = 0f;
        pauseMenu.SetActive(true);
        gameIsPaused = true;
        ShowTrophies(pauseMenu);
        ShowCoins();
        Time.timeScale = 0f;
    }
    //public IEnumerator PauseMenuWaitSeconds(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    Pause();
    //}
    private void ShowCoins()
    {
        menuScoreText = GameObject.FindWithTag("MenuScore").GetComponent<TextMeshProUGUI>();
        menuScoreText.text = "Score: "+mainScore.ToString();
    }

    public void WinMenu()
    {
        StartCoroutine(WinMenuWaitSeconds());  
    }

    private IEnumerator WinMenuWaitSeconds()
    {
        yield return new WaitForSeconds(2.0f);
        winMenu.SetActive(true);
        Time.timeScale = 0f;
        ShowTrophies(winMenu);
        ShowCoins();
    }
    public void ResetScore()
    {
        mainScore = 0;
    }
    public void AddScore()
    {
        mainScore += 2;
    }
    private void ResetTrophies(GameObject menu)
    {
        Transform trans = menu.transform;
        if (trophyCounter > 0)
        {
            Debug.Log("Trophy counter >0 funct");
            for (int i = 0; i < trophyCounter; i++)
            {
                Transform transTrophy = trans.Find("Keys/Key" + i.ToString());
                var tempColour = transTrophy.GetComponent<Image>().color;
                tempColour.a = 0.2f;
                transTrophy.gameObject.GetComponent<Image>().color = tempColour;
            }
        }
        trophyCounter = 0;
        trophyText = GameObject.FindWithTag("Trophy").GetComponent<TextMeshProUGUI>();
        trophyText.text = trophyCounter.ToString() + "/3";
    }
    private void ShowTrophies(GameObject menu)
    {
        Transform trans = menu.transform;
        if (trophyCounter > 0)
        {
            Debug.Log("Trophy counter >0 funct");
            for (int i = 0; i < trophyCounter; i++)
            {
                Transform transTrophy = trans.Find("Keys/Key" + i.ToString());
                var tempColour = transTrophy.GetComponent<Image>().color;
                tempColour.a = 0.5f;
                transTrophy.gameObject.GetComponent<Image>().color = tempColour;
            }
        }
        trophyText = GameObject.FindWithTag("Trophy").GetComponent<TextMeshProUGUI>();
        trophyText.text = trophyCounter.ToString() + "/3";
    }
    public void AddTrophy()
    {
        trophyCounter++;
    }
    //public void CompareTheHighestScore()
    //{
    //    if (highestScore < mainScore)
    //    {
    //        highestScore = mainScore;
    //    }
    //}
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
        startPosition = player.transform.position;
        ResetScore();
        ResetTrophies(pauseMenu);
        ResetTrophies(winMenu);
        //if (!winMenu.activeSelf || !pauseMenu.activeSelf)
        //{
        //    winMenu.SetActive(false);
        //    pauseMenu.SetActive(false);
        //}
    }
    public void InstantiateKeys()
    {
        GameObject keysParent = GameObject.Find("Keys");
        for (int i = 0; i < collectedKeys.Count; i++)
        {
            GameObject newKey= Instantiate(Key, collectedKeys[i], Quaternion.identity) as GameObject;
            newKey.transform.parent = keysParent.transform;
        }
    }
    public void ClearCollectedKeysArray()
    {
        collectedKeys.Clear();
    }
    public void InstantiateCoins()
    {
        GameObject coinsParent=GameObject.Find("Coins");
        for (int i = 0; i < collectedCoins.Count; i++)
        {
            GameObject newCoin=Instantiate(Coin, collectedCoins[i],Quaternion.identity) as GameObject;
            newCoin.transform.parent=coinsParent.transform;
        }
    }
    public void ClearCollectedCoinsArray()
    {
        collectedCoins.Clear();
    }
    public void PlayAgain()
    {
        InstantiateCoins();
        InstantiateKeys();
        ResetScore();
        ResetTrophies(pauseMenu);
        ResetTrophies(winMenu);
        StartCoroutine(RestartGame());
        gameIsPaused = false;
        ClearCollectedKeysArray();
        ClearCollectedCoinsArray();
    }
    public IEnumerator RestartGame()
    {
        if (player == null)
            Debug.LogWarning("player is missing");
        player.transform.position = startPosition;
        Time.timeScale = 1f;
        player.GetComponent<PlayerHealth>().ResetHealth();
        player.GetComponent<PlayerAnimationController>().PlayIdleAnim();
        yield return new WaitForSeconds(1f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
