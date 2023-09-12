using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseSetting : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private BGMPlayer bGMPlayer;
    [SerializeField] private GameObject soundSetting;
    public void OnClickCloseButton()
    {
        Application.Quit();
    }
    public void OnClickSoundButton()
    {
        soundSetting.SetActive(true);
        volumeSlider.value = bGMPlayer.Volume;
    }

    public void OnValueChangeSlider()
    {
        bGMPlayer.ChangeVolume(volumeSlider.value);
    }
}
