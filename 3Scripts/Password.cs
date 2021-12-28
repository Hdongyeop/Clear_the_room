using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    public bool isPassword;

    public Gamemanager gamemanager;
    public InputField inputField;
    public Button EnterButton;
    public Button CancelButton;
    public Image blackImage;
    public GameObject endingText;
    
    private void Start()
    {
        inputField.characterLimit = 4;
        // 정규표현식으로 0-9가 아니면 ""으로 바꿔버린다
        inputField.onValueChanged.AddListener(
            (word) => inputField.text = Regex.Replace(word, @"[^0-9]", "")
        );
    }

    private void OnEnable()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<Gamemanager>();
        gamemanager.pauseAble = false;
    }

    private void OnDisable()
    {
        gamemanager.pauseAble = true;
    }

    public void CheckPassword()
    {
        if (inputField.text == "7412")
        {
            CorrectPassword();
        }
        else
        {
            InCorrectPassword();
        }
    }
    
    void CorrectPassword()
    {
        Debug.Log("정답");
        inputField.gameObject.SetActive(false);
        EnterButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(FadeOut());
    }

    void InCorrectPassword()
    {
        Debug.Log("오답");
        inputField.text = "";
    }

    public void Cancel()
    {
        isPassword = false;
        inputField.text = "";
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        for (float i = 0; i <= 1; i+=0.01f)
        {
            Debug.Log("FadeOut");
            yield return new WaitForSeconds(0.1f);
            blackImage.color = new Color(0, 0, 0, i);
        }
        
        endingText.SetActive(true);
        Time.timeScale = 0f;
    }
    
}
