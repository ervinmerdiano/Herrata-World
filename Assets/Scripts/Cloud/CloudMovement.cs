using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 1f;
    private float resetX = -50.01f; // kiri
    private float startX = 50.01f;  // kanan

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, Random.Range(0.6f, 1f));
    }

    void Update()
    {

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < resetX)
        {
            float newY = Random.Range(-39.19f, 33.8f); // spawn ulang dengan ketinggian acak
            transform.position = new Vector3(startX, newY, 0f);
            sr.color = new Color(1f, 1f, 1f, Random.Range(0.6f, 1f));
        }
    }
}
