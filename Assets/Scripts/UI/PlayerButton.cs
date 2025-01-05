using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    public float scaleTo = 1.5f; // 放大的比例
    public float duration = 1f;  // 动画持续时间
    public Text startGame;
    public Button startButton;

    private void Awake()
    {
        startButton = GetComponent<Button>();
    }

    void Start()
    {
        // 开始放大和缩小的循环动画
        startGame.transform.DOScale(scaleTo, duration)
            .SetLoops(-1, LoopType.Yoyo);
        startButton.onClick.AddListener(StartGame
        );
    }

    private void StartGame()
    {
        //SceneManager.LoadScene("GamePlay");
        TransitionManager.instance.Transition("GamePlay");
    }

}
