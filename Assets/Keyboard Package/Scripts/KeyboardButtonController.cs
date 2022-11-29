using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardButtonController : MonoBehaviour
{
    [SerializeField] Image containerBorderImage;
    [SerializeField] Image containerFillImage;
    [SerializeField] Image containerIcon;
    [SerializeField] TextMeshProUGUI containerText;
    [SerializeField] TextMeshProUGUI containerActionText;


    public void AddLetter() {
        if(GameManager.Instance != null) {
            GameManager.Instance.AddLetter(containerText.text);
        } else {
            Debug.Log(containerText.text + " is pressed");
        }
    }
    public void DeleteLetter() { 
        if(GameManager.Instance != null) {
            GameManager.Instance.DeleteLetter();
        } else {
            Debug.Log("Last char deleted");
        }
    }
    public void SubmitWord() {
        if(GameManager.Instance != null) {
            GameManager.Instance.SubmitWord();
        } else {
            Debug.Log("Submitted successfully!");
        }
    }
}