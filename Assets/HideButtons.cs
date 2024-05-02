using System.Collections;
using UnityEngine;

public class HideButtons : MonoBehaviour
{
    public GameObject buttonsParent;

    void Update()
    {
        //If the screen is tapped, reset the timer to hide the buttons
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(ShowButtonsTemporarily());
        }
    }

    //Show the buttons for 5 seconds, then hide them
    IEnumerator ShowButtonsTemporarily()
    {
        buttonsParent.SetActive(true);
        yield return new WaitForSeconds(5);
        buttonsParent.SetActive(false);
    }
}
