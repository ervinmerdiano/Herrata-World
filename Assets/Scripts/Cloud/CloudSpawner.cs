using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public Sprite[] cloudSprites; // dari sprite sheet kamu
    public int cloudCount = 0;
    public float minX = -50.01f;
    public float maxX = 50.01f;
    public float minY = -39.19f;
    public float maxY = 33.19f;

    void Start()
    {
        for (int i = 0; i < cloudCount; i++)
        {
            GameObject cloud = Instantiate(cloudPrefab, new Vector3(
            Random.Range(minX - 20f, maxX + 20f),
            Random.Range(minY, maxY),
            0f
            ), Quaternion.identity);
            
            SpriteRenderer sr = cloud.GetComponent<SpriteRenderer>();

            // Random sprite dari list
            sr.sprite = cloudSprites[Random.Range(0, cloudSprites.Length)];

            // Random posisi
            float x = Random.Range(minX - 20f, maxX + 20f);
            float y = Random.Range(minY, maxY);

            // Random kecepatan sedikit biar beda tiap awan
            cloud.GetComponent<CloudMovement>().speed = Random.Range(0.5f, 2f);
        }
    }
}
