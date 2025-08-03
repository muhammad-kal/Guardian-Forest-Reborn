using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject GF_Logo;
    [SerializeField] private float floatHeight = 10f; 
    [SerializeField] private float floatDuration = 2f; 
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeInOutSine;

    [Header("FPS")]
    [SerializeField] private TextMeshProUGUI fps;

    private float deltatime = 0f;

    void Start()
    {
        Application.targetFrameRate = 60;
        FloatUpAndDown();
    }

    private void Update()
    {
        deltatime += (Time.unscaledDeltaTime - deltatime) * 0.1f;
        float frameCounter = 1.0f / deltatime;
        fps.text = "FPS : " + Mathf.RoundToInt(frameCounter).ToString();
    }

    void FloatUpAndDown()
    {
        LeanTween.moveLocalY(GF_Logo.gameObject, GF_Logo.transform.localPosition.y + floatHeight, floatDuration)
            .setEase(easeType)
            .setOnComplete(() =>
            {
                LeanTween.moveLocalY(GF_Logo.gameObject, GF_Logo.transform.localPosition.y-floatHeight, floatDuration)
                    .setEase(easeType)
                    .setOnComplete(FloatUpAndDown); 
            });
    }

    public void Keluar()
    {
        Application.Quit();
    }

    public void Lanjut()
    {
        FindAnyObjectByType<LoadingManagerScript>().LoadGame();
    }
}
