using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIController : MonoBehaviour
{
    public Image[] heartImages = new Image[3];
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    private PlayerController playerController;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (playerController == null)
        {
           
            playerController = GameObject.FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                UpdateHearts(); 
            }
        }
        else
        {
            UpdateHearts(); 
        }
    }
    
    void UpdateHearts()
    {
        int currentHP = playerController.GetCurrentHP();
        Debug.Log("현재 HP: " + currentHP);
        
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = (i < currentHP) ? fullHeart : emptyHeart;
        }
    }
}