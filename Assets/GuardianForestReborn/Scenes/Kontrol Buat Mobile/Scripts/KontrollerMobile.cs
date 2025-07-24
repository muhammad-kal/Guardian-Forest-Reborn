using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KontrollerMobile : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] RectTransform analogOutline;
    [SerializeField] RectTransform pentil;

    [Header("Settings")]
    private bool bisaKontrol;
    [SerializeField] float sensitivitas;
    private Vector3 bergerak;
    Vector3 posisiKlikAwal;


    [HideInInspector] public Vector3 GetBergerak => bergerak;
    [HideInInspector] public float magnitudePergerakan;

    private void Update()
    {
        if (bisaKontrol)
            ControlAnalog();

    }

    private void Start()
    {
        HideAnalog();
    }

    public void KlikDalamZonaCallback()
    {
        posisiKlikAwal = Input.mousePosition;
        analogOutline.transform.position = posisiKlikAwal;
        ShowAnalog();

    }

    private void ShowAnalog()
    {
        analogOutline.gameObject.SetActive(true);
        bisaKontrol = true;
    }

    private void HideAnalog()
    {
        analogOutline.gameObject.SetActive(false);
        bisaKontrol = false;
        magnitudePergerakan = 0;
        bergerak = Vector3.zero;
    }

    private void ControlAnalog()
    {
        Vector3 posisiKlikSaatIni = Input.mousePosition;
        Vector3 arah = posisiKlikSaatIni - posisiKlikAwal;

        magnitudePergerakan = arah.magnitude * sensitivitas / Screen.width;
        magnitudePergerakan = Mathf.Min(magnitudePergerakan, analogOutline.rect.width / 2);
        
        bergerak = magnitudePergerakan * arah.normalized;
        Vector3 posisiTarget = bergerak + posisiKlikAwal;

        pentil.position = posisiTarget;

        if (Input.GetMouseButtonUp(0))
            HideAnalog();
    }

    //[Header("Elements")]
    //[SerializeField] private RectTransform joystickOutline;
    //[SerializeField] private RectTransform joystickKnob;


    //[Header("Settings")]
    //[SerializeField] private float moveFactor;
    //private bool canControl;
    //private Vector3 clickedPosition;
    //private Vector3 move;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    HideJoystick();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (canControl)
    //    {
    //        ControlJoystick();
    //    }
    //}

    //public void ClickedOnJoystickZone()
    //{
    //    clickedPosition = Input.mousePosition;
    //    joystickOutline.position = clickedPosition;
    //    ShowJoystick();
    //}

    //private void ShowJoystick()
    //{
    //    joystickOutline.gameObject.SetActive(true);
    //    canControl = true;
    //}

    //private void HideJoystick()
    //{
    //    joystickOutline.gameObject.SetActive(false);
    //    canControl = false;
    //    move = Vector3.zero;
    //}

    //private void ControlJoystick()
    //{
    //    Vector3 currentPosition = Input.mousePosition;
    //    Vector3 direction = currentPosition - clickedPosition;

    //    float moveMagnitude = direction.magnitude * moveFactor / Screen.width;
    //    moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);
    //    move = direction.normalized * moveMagnitude;
    //    Vector3 tarrgetPosition = clickedPosition + move;

    //    joystickKnob.position = tarrgetPosition;

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        HideJoystick();
    //    }
    //}

    //public Vector3 GetMoveVector()
    //{
    //    return move;
    //}
}
