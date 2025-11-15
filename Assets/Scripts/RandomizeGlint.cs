using UnityEngine;

public class RandomizeGlint : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // Randomize animasi speed & start frame
        anim.speed = Random.Range(0.5f, 1.5f);
        anim.Play(0, -1, Random.Range(0f, 1f));

        // Mulai flicker opacity
        StartCoroutine(RandomizeOpacity());
    }

    System.Collections.IEnumerator RandomizeOpacity()
    {
        while (true)
        {
            // Opacity acak antara 0.4–1.0
            float targetAlpha = Random.Range(0.4f, 1f);
            float duration = Random.Range(0.3f, 1f); // seberapa cepat transisinya
            float startAlpha = sr.color.a;

            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
                yield return null;
            }

            // Tunggu sedikit sebelum ubah lagi
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        }
    }
}
