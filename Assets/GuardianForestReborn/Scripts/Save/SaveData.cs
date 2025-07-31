using System;
using System.Collections.Generic;

[Serializable]
public class EntityState
{
    public string nama;
    public string state;
}

[Serializable]
public class SaveData
{
    public List<EntityState> listEntity = new List<EntityState>();
    public float kecepatan;
    public int level;

    // Method untuk reset ke default awal
    public void ResetToDefault()
    {
        // listEntity = new List<EntityState>();
        // for (int i = 1; i <= 24; i++)
        // {
        //     listEntity.Add(new EntityState
        //     {
        //         nama = $"SpotPohon ({i})",
        //         state = "Tumbuh"
        //     });
        // }
        listEntity = new List<EntityState>
        {
            new EntityState { nama = "SpotPohon (1)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (2)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (3)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (4)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (5)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (6)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (7)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (8)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (9)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (10)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (11)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (12)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (13)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (14)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (15)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (16)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (17)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (18)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (19)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (20)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (21)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (22)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (23)", state = "Tumbuh" },
            new EntityState { nama = "SpotPohon (24)", state = "Tumbuh" },
        };

        kecepatan = 3.0f;
        level = 1;
    }
}
