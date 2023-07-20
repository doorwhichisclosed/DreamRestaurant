using UnityEngine;

public class Archive : MonoBehaviour
{
    public GameObject lockImage;
    public GameObject unlockImage;
    public GameObject costText;
    [SerializeField] private int level;
    public int Level { get { return level; } }
}
