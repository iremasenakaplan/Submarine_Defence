using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagSelectionManager : MonoBehaviour
{
    public GameObject flagPanel; 
    public Button flagButton;    
    public Image flagImage;    


    private void Start()
    {
        flagButton.onClick.AddListener(OpenFlagPanel);
    }

    private void OpenFlagPanel()
    {
        flagPanel.SetActive(true);
    }

    public void ChangeFlagImage(Sprite newFlag)
    {
        flagImage.sprite = newFlag;
    }
}
