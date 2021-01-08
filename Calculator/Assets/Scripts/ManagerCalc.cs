using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerCalc : MonoBehaviour
{
    public VerticalLayoutGroup buttonGroup;
    public HorizontalLayoutGroup bottomRow;
    public RectTransform canvasRect;
    CalcButton[] bottomButtons;

    public Text digitLabel;
    public Text operatorLabel;
    public Text digitHistory;
    bool errorDisplayed;
    bool displayValid;
    bool specialAction;
    double currentVal;
    double storedVal;
    double result;
    char storedOperator;
    string hist;

    private void Awake()
    {
        bottomButtons = bottomRow.GetComponentsInChildren<CalcButton>();
    }

    void Start()
    {
        bottomRow.childControlWidth = false;
        buttonTapped('c');
    }

    void Update()
    {
        
    }

    void clearCalc()
    {
        digitLabel.text = "0";
        operatorLabel.text = "";
        digitHistory.text = "";
        specialAction = displayValid = errorDisplayed = false;
        currentVal = result = storedVal = 0;
        storedOperator = ' ';
    }
    void updateDigitLabel()
    {
        if (!errorDisplayed)
            digitLabel.text = currentVal.ToString();
        displayValid = false;
    }

    void calcResult(char activeOp)
    {
        switch (activeOp)
        {
            case '=':
                result = currentVal;
                //digitHistory.text += result;
                break;
            case '+':
                result = storedVal + currentVal;
                break;
            case '-':
                result = storedVal - currentVal;
                break;
            case '×':
                result = storedVal * currentVal;
                break;
            case '÷':
                if (currentVal != 0)
                {
                    result = storedVal / currentVal;
                }
                else
                {
                    errorDisplayed = true;
                    digitLabel.text = "ERROR";
                }
                break;
            default:
                Debug.Log("unknown: " + activeOp);
                break;
        }
        currentVal = result;
        updateDigitLabel();
    }

    public void buttonTapped(char caption)
    {
        if (errorDisplayed)
            clearCalc();

        if ((caption >= '0' && caption <= '9') || caption == '.')
        {
            if (digitLabel.text.Length < 15 || !displayValid)
            {
                if (!displayValid)
                    digitLabel.text = (caption == '.' ? "0" : "");
                else if (digitLabel.text == "0" && caption != '.')
                    digitLabel.text = "";
                digitLabel.text += caption;

                //digitHistory.text += caption;
                displayValid = true;
            }
        }
        else if (caption == 'c')
        {
            clearCalc();
        }
        else if (caption == '±')
        {
            currentVal = -double.Parse(digitLabel.text);
            updateDigitLabel();
            specialAction = true;
        }
        else if (caption == '%')
        {
            currentVal = double.Parse(digitLabel.text) / 100.0d;
            updateDigitLabel();
            specialAction = true;
        }
        else if (displayValid || storedOperator == '=' || specialAction)
        {
            currentVal = double.Parse(digitLabel.text);
            displayValid = false;
            
            if (storedOperator != ' ')
            {
                calcResult(storedOperator);
                storedOperator = ' ';
            }
            operatorLabel.text = caption.ToString();

            //digitHistory.text += caption.ToString();
            
            storedOperator = caption;
            storedVal = currentVal;
            updateDigitLabel();
            specialAction = false;
        }
    }
    public void buttonTappedTo(string caption)
    {
        if (caption == "x³")
        {
            currentVal = double.Parse(digitLabel.text);
            currentVal = Mathf.Pow((float)currentVal, 3);
            updateDigitLabel();
            specialAction = true;
        }
        else if(caption == "x²")
        {
            currentVal = double.Parse(digitLabel.text);
            currentVal = Mathf.Pow((float)currentVal, 2);
            updateDigitLabel();
            specialAction = true;
        }
        else if(caption == "²√x")
        {
            currentVal = double.Parse(digitLabel.text);
            currentVal = Mathf.Sqrt((float)currentVal);
            updateDigitLabel();
            specialAction = true;
        }
        else if (caption == "³√x") 
        {
            currentVal = double.Parse(digitLabel.text);
            currentVal = Mathf.Sqrt((float)currentVal);
            updateDigitLabel();
            specialAction = true;
        }
        else if(caption == "10ˣ") 
        {
            currentVal = double.Parse(digitLabel.text);
            currentVal = Mathf.Pow(10, (float)currentVal);
            updateDigitLabel();
            specialAction = true;
        }
        else if (caption == "1/x")
        {
            currentVal = 1 / double.Parse(digitLabel.text);
            updateDigitLabel();
            specialAction = true;
        }
    }
    public void onHistory(char caption)
    {
        if (caption == 'c') { }
       
        else if (caption != '=')
        {
            hist += caption;
        } 
        else if(caption == '=') 
        {
            digitHistory.text += hist;
            digitHistory.text += "=";
            digitHistory.text += digitLabel.text;
            hist = String.Empty;
        }
    }
}
