using TMPro; 
using UnityEngine;

public class IncreaseFontSizeTMP : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent; 
    public int increaseAmount = 2; //Increases the font size by 2 units at a time

    public void IncreaseTextFontSize()
    {
        textMeshProComponent.fontSize += increaseAmount;
    }
}