using UnityEngine;

public class TouchEffectManager : BasePool<TouchEffect>
{
    float spawnTime;
    public float defaultTime = 0.05f;
    public GameObject effectContainer;
    public Canvas parentCanvas;

    void Update()
    {
        if (Input.GetMouseButton(0) && spawnTime >= defaultTime)
        {
            GetFromPool();
            spawnTime = 0;
        }
        spawnTime += Time.deltaTime;
    }
    protected override void OnRelease(TouchEffect poolingObject)
    {
        base.OnRelease(poolingObject);
    }
    protected override void OnGet(TouchEffect poolingObject)
    {
        poolingObject.transform.SetParent(effectContainer.transform);
        Vector2 mPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out mPosition);
        poolingObject.transform.localPosition = mPosition;
        poolingObject.InitTouchEffect();
        base.OnGet(poolingObject);
    }
}
