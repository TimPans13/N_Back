using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    private int buttonID;
    public int ButtonID { get => buttonID; set => buttonID = value; }
    private GameManager gameManager;
    [SerializeField]private Image buttonImage;
    public Color flashColor = Color.blue; 

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ///buttonImage = GetComponent<Image>();

    }
    public void ButtonClick()
    {
        gameManager.OnButtonClicked(buttonID);
    }

    public void FlashButton()
    {
        StartCoroutine(FlashButtonCoroutine());
    }

    private IEnumerator FlashButtonCoroutine()
    {
        Color originalColor = buttonImage.color; 
        buttonImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 1.0f); 
        yield return new WaitForSeconds(gameManager.buttonFlashDuration);
        buttonImage.color = originalColor; 
    }
}
