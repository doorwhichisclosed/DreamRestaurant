using UnityEngine;
using UnityEngine.Pool;

public class TouchEffect : MonoBehaviour, IBasePoolingObject<TouchEffect>
{
    SpriteRenderer spriteRenderer;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);
        spriteRenderer.color = colors[Random.Range(0, colors.Length)];
    }
    private void Update()
    {
        transform.Translate(direction * moveSpeed);
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);
        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(spriteRenderer.color.a, 0, Time.deltaTime * colorSpeed);
        spriteRenderer.color = color;
        if (spriteRenderer.color.a <= 0.01f)
            Release();
    }
}
