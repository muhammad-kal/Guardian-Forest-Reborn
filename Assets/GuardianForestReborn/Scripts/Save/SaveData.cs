using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EntityState
{
    public string nama;
    public string state;
}

[Serializable]
public class SaveData
{
    public List<EntityState> listEntity = new();

    [NonSerialized]
    public Dictionary<string, EntityState> entityMap = new();

    public float kecepatan;
    public int level;

    public void BuildEntityMap()
    {
        entityMap.Clear();
        foreach (var entity in listEntity)
        {
            entityMap[entity.nama] = entity;
        }
    }

    public void ResetToDefault()
    {
        listEntity = new List<EntityState>();

        for (int i = 1; i <= 24; i++)
        {
            listEntity.Add(new EntityState
            {
                nama = $"SpotPohon ({i})",
                state = (i == 24) ? "Tanam" : "Tidak Tanam"
            });
        }

        kecepatan = 3.0f;
        level = 1;

        BuildEntityMap();
    }
    public EntityState GetEntity(string nama)
    {
        if (entityMap.TryGetValue(nama, out var entity))
            return entity;

        Debug.LogWarning($"Entity dengan nama '{nama}' tidak ditemukan.");
        return null;
    }
    public void SetState(string nama, string stateBaru)
    {
        if (entityMap.TryGetValue(nama, out var entity))
        {
            entity.state = stateBaru;
        }
        else
        {
            Debug.LogWarning($"Gagal mengubah state: Entity dengan nama '{nama}' tidak ditemukan.");
        }
    }
}
