using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public List<Sprite> customerImages;
    public void SetCustomer()
    {
        int r =Random.Range(0,customerImages.Count);
        GetComponent<Image>().sprite = customerImages[r];
        GetComponent<Image>().preserveAspect = true;
    }
}
