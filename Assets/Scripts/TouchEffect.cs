using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class TouchEffect : MonoBehaviour, IBasePoolingObject<TouchEffect>
{
    Image imageRenderer;
    Vector2 direction;
    public float moveSpeed;
    public float minSize = 0.1f;
    public float maxSize = 0.3f;
    public float sizeSpeed = 1;
    public float colorSpeed = 5;
    public Color[] colors;
    private IObjectPool<TouchEffect> touchEffectPool;
    public void SetPool(IObjectPool<TouchEffect> _pool)
    {
        this.touchEffectPool = _pool;
    }
    public void Release()
    {
        if (this.isActiveAndEnabled)
        {
            touchEffectPool.Release(this);
        }
    }
    public void InitTouchEffect()
    {
        imageRenderer = GetComponent<Image>();
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);
        imageRenderer.color = colors[Random.Range(0, colors.Length)];
    }
    private void Update()
    {
        transform.Translate(direction * moveSpeed);
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);
        Color color = imageRenderer.color;
        color.a = Mathf.Lerp(imageRenderer.color.a, 0, Time.deltaTime * colorSpeed);
        imageRenderer.color = color;
        if (imageRenderer.color.a <= 0.01f)
            Release();
    }
}
