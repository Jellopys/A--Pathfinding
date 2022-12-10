using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stat : MonoBehaviour
{
    private Image content;
    private float lerpSpeed = 1f;
    private float currentFillAmount;
    [SerializeField] private TextMeshProUGUI statValue;

    public float MyMaxValue { get; set; }
    private float currentValue;
    public float MyCurrentValue 
    { 
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFillAmount = currentValue / MyMaxValue;

            statValue.text = currentValue + " / " + MyMaxValue;
        }
    }

    void Start()
    {
        MyMaxValue = 100f;
        content = GetComponent<Image>();
    }

    
    void Update()
    {
        if (currentFillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFillAmount, Time.deltaTime * lerpSpeed);
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
