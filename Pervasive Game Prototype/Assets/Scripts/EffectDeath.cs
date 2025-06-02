using UnityEngine;
using UnityEngine.UI;

public class EffectDeath : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] public float rotationSpeed;

    private float timer;
    private Image image;
    private Color originalColor;

    void Start()
    {
        timer = 0f;
        image = GetComponent<Image>();
        originalColor = image.color;

        Invoke("Death", lifetime);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime * 360));

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / lifetime);
        float alpha = 1f - t;
        Color c = originalColor;
        c.a = alpha;
        image.color = c;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
