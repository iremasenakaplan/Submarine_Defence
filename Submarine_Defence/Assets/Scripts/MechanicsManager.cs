using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MechanicsManager : MonoBehaviour
{
    public static MechanicsManager Instance;
    float cameraInitialFov;
    [SerializeField] TMP_Text zoomText;
    [SerializeField] Slider zoomSlider;
    float playingOffset = 0;
    float playingLimit = 0.5f;
    [SerializeField] AudioSource zoomScrollAudioSource ;

    void Awake(){
        if(Instance==null)
            Instance = this;
    }

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

    public void SetMaxZoom(int maxZoom){
        zoomSlider.maxValue = maxZoom;
        zoomSlider.minValue = -maxZoom;
    }

    public void ZoomControl( ) {
        Camera.main.fieldOfView = cameraInitialFov + zoomSlider.value;
        zoomText.text = ((int)(((int)(-zoomSlider.value) + zoomSlider.maxValue)/8)).ToString() + "X";
        if (!zoomScrollAudioSource.enabled)
        {
            playingOffset = 0f;
            zoomScrollAudioSource.enabled = true;
        }
    }
}
