using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkillMenanamPohon : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpotPohonManager spotPohonManager;

    private void Start()
    {
        SpotPohon.ActiondalamAreaPohon += SudahDalamAreaPohon;
    }

    private void OnDestroy()
    {
        SpotPohon.ActiondalamAreaPohon -= SudahDalamAreaPohon;

    }

    private void SudahDalamAreaPohon(SpotPohon spotPohon)
    {
        spotPohonManager.DalamAreaSpotPohon(spotPohon);
    }

}
