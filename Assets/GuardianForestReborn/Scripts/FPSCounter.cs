using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    [Header("FPS")]
    [SerializeField] private TextMeshProUGUI fps;

    private float deltatime = 0f;

    void Update()
    {
        deltatime += (Time.unscaledDeltaTime - deltatime) * 0.1f;
        float frameCounter = 1.0f / deltatime;
        fps.text = "FPS : " + Mathf.RoundToInt(frameCounter).ToString();
    }
}
