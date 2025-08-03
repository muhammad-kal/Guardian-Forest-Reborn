using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManagerScript : MonoBehaviour
{
    [Header("Loading Elements")]
    [SerializeField] private GameObject loadingScene;

    [SerializeField]private Slider loadingBar;
    [SerializeField] private GameObject loadingBar2;
    [SerializeField] private Material skyboxGame;


    private void Start()
    {
        loadingScene.SetActive(false);
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            return;

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    //0 Loading, 1 Menu, 2 Game
    public void LoadGame()
    {
        loadingScene.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(1));
        scenesLoading.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = totalSceneProgress / scenesLoading.Count;
                print(totalSceneProgress);
                loadingBar.value = totalSceneProgress;

                yield return null;
            }
        
        }
        RenderSettings.skybox = skyboxGame;

        loadingScene.SetActive(false);
    }
}
