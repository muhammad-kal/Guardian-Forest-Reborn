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
    [SerializeField] private Animator animator;

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
        StartCoroutine(LerpLayerWeight(1, 0f, 0.4f, 1f));
        animator.SetTrigger("Berdiri"); // tetap pakai trigger biar animasi transisi jalan
    }

    IEnumerator LerpLayerWeight(int layerIndex, float start, float end, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float weight = Mathf.Lerp(start, end, t / duration);
            animator.SetLayerWeight(layerIndex, weight);
            yield return null;
        }
        animator.SetLayerWeight(layerIndex, end);
        yield return new WaitForSeconds(1.2f);
        FindAnyObjectByType<LoadingManagerScript>().LoadGame();
    }

}
