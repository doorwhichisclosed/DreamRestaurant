using UnityEngine;

public class TouchEffectManager : BasePool<TouchEffect>
{
    float spawnTime;
    public float defaultTime = 0.05f;

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
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        poolingObject.transform.localPosition = mPosition;
        poolingObject.InitTouchEffect();
        base.OnGet(poolingObject);
    }
}
