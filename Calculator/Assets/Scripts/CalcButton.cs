using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcButton : MonoBehaviour
{

	public Text label;

    public RectTransform rectTransform
    {
        get
        {
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }
    RectTransform _rectTransform;

    public ManagerCalc calcManager
    {
        get
        {
            if (_calcManager == null)
                _calcManager = GetComponentInParent<ManagerCalc>();
            return _calcManager;
        }
    }
    static ManagerCalc _calcManager;

    public void onTapped()
    {
        Debug.Log("Tapped: " + label.text);
        calcManager.buttonTapped(label.text[0]);
        calcManager.onHistory(label.text[0]);
    }
    public void onTappedto()
    {
        Debug.Log("Tapped: " + label.text);
        calcManager.buttonTappedTo(label.text);
    }
}
