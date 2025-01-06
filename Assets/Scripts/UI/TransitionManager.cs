using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;
    private CanvasGroup _canvasGroup;
    public float scaler;

    private void Awake()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
        

    }

    private void Start()
    {
        StartCoroutine(Fade(0));
    }
    public void Transition(string sceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(TransitionToScene(sceneName));
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        yield return Fade(1);
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return Fade(0);
    }

    /// <summary>
    /// 执行一个渐变动画，将UI元素的透明度从当前值逐渐改变到指定的值。
    /// </summary>
    /// <param name="amount">目标透明度值，只能是0或1。</param>
    /// <returns>一个IEnumerator对象，用于在Unity的协程中控制动画的播放。</returns>
    private IEnumerator Fade(int amount)
    {
        // 在渐变动画开始时，阻止UI元素响应用户交互，以避免动画过程中用户的干扰。
        _canvasGroup.blocksRaycasts = true;
    
        // 循环调整UI元素的透明度，直到达到目标值。
        while (_canvasGroup.alpha != amount)
        {
            // 根据目标透明度值，选择性地增加或减少当前透明度。
            switch (amount)
            {
                case 1:
                    // 如果目标透明度为1，则逐渐使UI元素变得不透明。
                    _canvasGroup.alpha += Time.deltaTime * scaler;
                    break;
                case 0:
                    // 如果目标透明度为0，则逐渐使UI元素变得完全透明。
                    _canvasGroup.alpha -= Time.deltaTime * scaler;
                    break;
            }
    
            // 在每次循环结束时，将控制权交还给Unity引擎，以确保其他游戏逻辑可以继续执行。
            yield return null;
        }
    
        // 在渐变动画结束后，允许UI元素再次响应用户交互。
        _canvasGroup.blocksRaycasts = false;
    }
}
