using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LadangState { Kosong, Tertanam, Tersiram }

public class TanahLadang : MonoBehaviour
{
    private LadangState state;

    [Header("Settings")]
    [SerializeField] private Transform tanamanParent;
    [SerializeField] private MeshRenderer tanahLadangRenderer;
    private Tanaman tanaman;



    private void Start()
    {
        state = LadangState.Kosong;
    }

    public void TaburBibit(TanamanData dataTanaman)
    {
        state = LadangState.Tertanam;

        tanaman = Instantiate(dataTanaman.tanaman, transform.position, Quaternion.identity, tanamanParent);


        //GameObject tanaman = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //tanaman.transform.position = transform.position;
        //tanaman.transform.localScale = Vector3.one / 2;
        //tanaman.GetComponent<Collider>().enabled = false;
    }

    public void SiramAir()
    {
        state = LadangState.Tersiram;
        //tanahLadangRenderer.material.color = Color.white * .2f;
        tanaman.TanamanTumbuh();

        tanahLadangRenderer.gameObject.LeanColor(Color.white * .2f, 1);

        //StartCoroutine("TanahBerubahWarna");
    }

    //IEnumerator TanahBerubahWarna()
    //{
    //    float durasi = 1;
    //    float timer = 0;
    //    while (timer < durasi)
    //    {
    //        float t = timer / durasi;
    //        Color warnaTanahLerp = Color.Lerp(Color.white, Color.white * .2f, t);
    //        tanahLadangRenderer.material.color = warnaTanahLerp;
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    yield return null;
    //}

    public bool isKosong()
    {
        return state == LadangState.Kosong;
    }

    public bool isTertanam()
    {
        return state == LadangState.Tertanam;
    }

    public bool isTersiramSemua()
    {
        Debug.Log(state);
        return state == LadangState.Tersiram;
    }
}
