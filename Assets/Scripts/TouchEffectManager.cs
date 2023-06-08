using UnityEngine;

public class TouchEffectManager : BasePool<TouchEffect>
{
    float spawnTime;
    public float defaultTime = 0.05f;
    public GameObject effectContainer;
    public RectTransform parentCanvas;

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
        Vector3 mPosition = Input.mousePosition;
        mPosition.z = 0;
        poolingObject.transform.position = mPosition;
        poolingObject.InitTouchEffect();
        base.OnGet(poolingObject);
    }
}
