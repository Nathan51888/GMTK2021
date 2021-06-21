using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : GenericSingleton<GameManager>
{
    public enum GameScenes
    {
        Start,
        Level,
        Loading,
        Menu
    }

    public int totalLevelCount;
    public int currentLevel;
    public int deathCount;
    public int totalFoodCount;
    private Action _onLoaderCallback;
    private AsyncOperation _loadingAsyncOperation;

    public void StartGame()
    {
        LoadScene(GameScenes.Start);
    }

    public void Respawn()
    {
        deathCount++;
        totalFoodCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadSceneAsync(GameScenes scenes)
    {
        yield return null;

        _loadingAsyncOperation = SceneManager.LoadSceneAsync(scenes.ToString());

        while (!_loadingAsyncOperation.isDone)
            yield return null;
    }

    private IEnumerator LoadSceneAsync(GameScenes scenes, int levelNum)
    {
        yield return null;

        _loadingAsyncOperation = SceneManager.LoadSceneAsync(scenes.ToString() + levelNum);

        while (!_loadingAsyncOperation.isDone)
            yield return null;
    }

    public float GetLoadingProgress()
    {
        return _loadingAsyncOperation?.progress ?? 1f;
    }

    public void LoadScene(GameScenes scenes)
    {
        _onLoaderCallback = () =>
        {
            switch (scenes)
            {
                case GameScenes.Menu:
                    currentLevel = 0;
                    totalFoodCount = 0;
                    StartCoroutine(LoadSceneAsync(GameScenes.Menu));
                    return;

                case GameScenes.Level:
                    currentLevel += 1;
                    totalFoodCount = 0;
                    if (currentLevel >= totalLevelCount + 1)
                    {
                        currentLevel = 0;
                        totalFoodCount = 0;
                        StartCoroutine(LoadSceneAsync(GameScenes.Menu));
                        return;
                    }

                    StartCoroutine(LoadSceneAsync(GameScenes.Level, currentLevel));
                    return;

                case GameScenes.Start:
                    currentLevel = 1;
                    totalFoodCount = 0;
                    StartCoroutine(LoadSceneAsync(GameScenes.Level, currentLevel));
                    return;
            }
        };

        currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(GameScenes.Loading.ToString());
    }

    public void LoaderCallback()
    {
        if (_onLoaderCallback != null)
        {
            _onLoaderCallback();
            _onLoaderCallback = null;
        }
    }
}