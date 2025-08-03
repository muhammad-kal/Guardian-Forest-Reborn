using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingManager : MonoBehaviour
{
    

    [Header("Animasi Elements")]
    [SerializeField] private UnityEngine.UI.Image panelImage;

    [SerializeField] float fadeDuration = 1.0f;
    [SerializeField] float minAlpha = 90f / 255f;
    [SerializeField] float maxAlpha = 140f / 255f;

    void Start()
    {
        AnimasiLoading();
        
    }

    private void AnimasiLoading()
    {
        Color c = panelImage.color;
        c.a = minAlpha;
        panelImage.color = c;

        LeanTween.value(gameObject, minAlpha, maxAlpha, fadeDuration)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnUpdate((float val) => {
                Color temp = panelImage.color;
                temp.a = val;
                panelImage.color = temp;
            })
            .setLoopPingPong();
    }
}
