using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.XR.ARFoundation; 

public class CameraControl : MonoBehaviour
{
    public ARSession arSession; 
    public Button toggleButton; 

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleCamera); //Call ToggleCamera when the button is pressed
    }

    void ToggleCamera()
    {
        if (arSession.enabled)
        {
            arSession.enabled = false; //Disable AR
        }
        else
        {
            arSession.enabled = true; //Enable AR
        }
    }
}