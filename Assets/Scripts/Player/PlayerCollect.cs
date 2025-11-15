using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PlayerCollect : MonoBehaviour
{
    [Header("Tilemap & Collection Settings")]
    public Tilemap cropTilemap;  // assign Tilemap Vegetables
    public int cropsCollected = 0;

    [Header("Audio")]
    public AudioSource collectSound;

    // HashSet untuk track posisi “base tile” yang sudah di-harvest
    private HashSet<Vector3Int> harvestedBaseTiles = new HashSet<Vector3Int>();

    // Offset dari player ke base tile crop (misal player pivot di center, crop tinggi)
    public Vector3 baseTileOffset = new Vector3(0f, -0.5f, 0f);

    private void Update()
    {
        // Tentukan posisi world dari base tile tanaman
        Vector3 baseWorldPos = transform.position + baseTileOffset;
        Vector3Int baseCellPos = cropTilemap.WorldToCell(baseWorldPos);

        // cek kalau base tile ada dan belum pernah di-harvest
        if (cropTilemap.HasTile(baseCellPos) && !harvestedBaseTiles.Contains(baseCellPos))
        {
            cropsCollected++;
            Debug.Log("Crop harvested: " + cropsCollected);

            // Hapus semua tile yang menempati posisi crop ini (bisa tinggi)
            RemoveCropTiles(baseCellPos);

            // tandai base tile sudah di-harvest
            harvestedBaseTiles.Add(baseCellPos);

            // Mainin suara collect
            if (collectSound != null)
            {
                collectSound.Play();
            }
        }
    }

    private void RemoveCropTiles(Vector3Int baseCellPos)
    {
        // Hapus base tile dan beberapa tile di atasnya (misal tinggi 2)
        int cropHeight = 2; // ubah sesuai tinggi tanamanmu
        for (int i = 0; i < cropHeight; i++)
        {
            Vector3Int pos = new Vector3Int(baseCellPos.x, baseCellPos.y + i, baseCellPos.z);
            if (cropTilemap.HasTile(pos))
            {
                cropTilemap.SetTile(pos, null);
            }
        }
    }
}
