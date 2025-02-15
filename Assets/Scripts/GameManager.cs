using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private GameObject restartPanel;

    [SerializeField] private TMP_Text playerChancesText;

    private int playerChances = 3;

    private bool isGameEnded = false;

    public bool IsGameEnded {  get { return isGameEnded; } }

    private void Start()
    {
        restartPanel.SetActive(false);
        ShowPlayerChances();
    }

    private void ShowPlayerChances()
    {
        playerChancesText.text = playerChances.ToString();
    }

    public void PlayerLoseChance()
    {
        playerChances--;
        ShowPlayerChances();
        if (playerChances == 0)
        {
            isGameEnded = true;
            restartPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
