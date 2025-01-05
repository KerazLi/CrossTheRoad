using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EventHandler = Utilities.EventHandler;

public class UIManager : MonoBehaviour
{
    public Text scoretext;
    public GameObject gameOverPanel ;

    private void OnEnable()
    {
        EventHandler.GetPointEvent += OnGetPointEvent;
        EventHandler.GameOverEvent += OnGameOverEvent;
        Time.timeScale = 1;
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);
        if (gameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    private void Start()
    {
        scoretext.text = "00";
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
        EventHandler.GameOverEvent -= OnGameOverEvent;
    }

    private void OnGetPointEvent(int obj)
    {
        scoretext.text = obj.ToString();
    }

    public void RestGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
