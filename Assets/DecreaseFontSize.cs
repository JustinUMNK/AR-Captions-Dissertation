using TMPro; 
using UnityEngine;

public class DecreaseFontSizeTMP : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent; 
    public int DecreaseAmount = 2; //Decreases the font size by 2 units at a time

    public void DecreaseTextFontSize()
    {
        textMeshProComponent.fontSize -= DecreaseAmount;
    }
}