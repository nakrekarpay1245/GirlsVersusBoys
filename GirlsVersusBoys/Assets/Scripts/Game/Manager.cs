using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    [Header("ECONOMY")]
    [SerializeField]
    private float money;

    [SerializeField]
    private TextMeshProUGUI moneyText;


    public static Manager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Update()
    {
        #region ECONOMY

        moneyText.text = money.ToString();

        #endregion
    }

    #region ECONOMY
    public void IncreaseMoney(float _value)
    {
        money += _value;
    }

    #endregion
}
