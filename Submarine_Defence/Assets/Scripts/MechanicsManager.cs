using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MechanicsManager : MonoBehaviour
{
    float cameraInitialFov;
    [SerializeField] TMP_Text zoomText;
    [SerializeField] Slider zoomSlider;
    float playingOffset = 0;
    float playingLimit = 0.5f;
    [SerializeField] AudioSource zoomScrollAudioSource ;

    private void Start()
    {
        cameraInitialFov = Camera.main.fieldOfView;
    }

    private void Update()
    {
        playingOffset += Time.deltaTime;
        if (playingOffset > playingLimit && zoomScrollAudioSource.enabled)
            zoomScrollAudioSource.enabled = false;
    }

    public void ZoomControl( ) {
        Camera.main.fieldOfView = cameraInitialFov + zoomSlider.value;
        zoomText.text = (((int)(-zoomSlider.value) + 20)/8).ToString() + "X";
        if (!zoomScrollAudioSource.enabled)
        {
            playingOffset = 0f;
            zoomScrollAudioSource.enabled = true;
        }
    }
}
