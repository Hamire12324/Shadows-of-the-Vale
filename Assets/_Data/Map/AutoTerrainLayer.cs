using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AutoTerrainLayer : MonoBehaviour
{
    [Header("Terrain")]
    public Terrain terrain;

    [Header("Terrain Layers")]
    public TerrainLayer grassA;
    public TerrainLayer grassB;
    public TerrainLayer pebblesB;
    public TerrainLayer cliffMossyE;

    [Header("Slope Parameters")]
    [Range(0, 90)] public float cliffSlope = 45f;
    [Range(0, 90)] public float midSlope = 20f;

    [Header("Blend Softness")]
    public float slopeBlend = 10f;

    [Header("Grass Noise")]
    public float grassNoiseScale = 12f;
    public int seed = 12345;

    public void Apply()
    {
        if (!Validate()) return;

        TerrainData td = terrain.terrainData;
        EnsureAllLayersInTerrain(td);

        int res = td.alphamapResolution;
        int layerCount = td.terrainLayers.Length;

        float[,,] maps = new float[res, res, layerCount];

        Random.InitState(seed);

        int cliffIdx = GetLayerIndex(td, cliffMossyE);
        int pebbleIdx = GetLayerIndex(td, pebblesB);
        int grassAIdx = GetLayerIndex(td, grassA);
        int grassBIdx = GetLayerIndex(td, grassB);

        for (int y = 0; y < res; y++)
        {
            for (int x = 0; x < res; x++)
            {
                float normX = (float)x / (res - 1f);
                float normY = (float)y / (res - 1f);

                float slope = td.GetSteepness(normX, normY);

                // ───────────────────────────────
                // WEIGHTS
                // ───────────────────────────────
                float wCliff = Mathf.Clamp01((slope - cliffSlope) / slopeBlend);
                float wPebble = Mathf.Clamp01((slope - midSlope) / slopeBlend) * (1f - wCliff);

                // phần còn lại là grass
                float baseGrass = Mathf.Max(0f, 1f - (wCliff + wPebble));

                // noise chia grass A/B
                float n = Mathf.PerlinNoise(normX * grassNoiseScale + seed, normY * grassNoiseScale + seed);
                float wGrassA = baseGrass * Mathf.Lerp(0.1f, 0.9f, n);
                float wGrassB = baseGrass - wGrassA;

                // ───────────────────────────────
                // GÁN WEIGHT
                // ───────────────────────────────
                maps[y, x, cliffIdx] = wCliff;
                maps[y, x, pebbleIdx] = wPebble;
                maps[y, x, grassAIdx] = wGrassA;
                maps[y, x, grassBIdx] = wGrassB;

                // Chuẩn hoá tổng = 1
                float sum = wCliff + wPebble + wGrassA + wGrassB;
                if (sum > 0f)
                {
                    maps[y, x, cliffIdx] /= sum;
                    maps[y, x, pebbleIdx] /= sum;
                    maps[y, x, grassAIdx] /= sum;
                    maps[y, x, grassBIdx] /= sum;
                }
            }
        }

        td.SetAlphamaps(0, 0, maps);
        Debug.Log("Terrain layers applied successfully (no-water version).");
    }

    // ───────────────────────────────────────────────
    bool Validate()
    {
        if (terrain == null) { Debug.LogError("Terrain is NULL"); return false; }
        if (terrain.terrainData == null) { Debug.LogError("TerrainData is NULL"); return false; }

        if (grassA == null || grassB == null || pebblesB == null || cliffMossyE == null)
        {
            Debug.LogError("Some TerrainLayers are NULL!");
            return false;
        }
        return true;
    }

    // ───────────────────────────────────────────────
    void EnsureAllLayersInTerrain(TerrainData td)
    {
        List<TerrainLayer> list = td.terrainLayers.ToList();

        TerrainLayer[] needed = { grassA, grassB, pebblesB, cliffMossyE };

        bool changed = false;

        foreach (TerrainLayer l in needed)
        {
            if (!list.Contains(l))
            {
                list.Add(l);
                changed = true;
            }
        }

        if (changed)
        {
            td.terrainLayers = list.ToArray();
            Debug.Log("Added missing TerrainLayers to TerrainData.");
        }
    }

    // ───────────────────────────────────────────────
    int GetLayerIndex(TerrainData td, TerrainLayer layer)
    {
        for (int i = 0; i < td.terrainLayers.Length; i++)
            if (td.terrainLayers[i] == layer)
                return i;

        Debug.LogError("TerrainLayer not found: " + layer.name);
        return 0;
    }
}
