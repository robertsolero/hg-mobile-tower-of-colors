using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterPreviewUI : MonoBehaviour
{
    [SerializeField] 
    private Image image;
    [SerializeField] 
    private TMP_Text amountText;

    public void Setup(Sprite boosterSprite, int amount)
    {
        image.sprite = boosterSprite;
        amountText.text = amount.ToString();
    }
}