using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManageUIValues : MonoBehaviour
{
    [SerializeField] private IntVariable _currentTreats;
    [SerializeField] private IntVariable _currentCats;
    [SerializeField] private FloatVariable _currentPassiveIncome;
    [SerializeField] private IntVariable _perStepIncome;
    [SerializeField] private IntVariable _catPrice;
    [SerializeField] private IntVariable _catUpgradePrice;
    [SerializeField] private IntVariable _stepUpgradePrice;


    [SerializeField] private TextMeshProUGUI _treatsText;
    [SerializeField] private TextMeshProUGUI _catsText;
    [SerializeField] private TextMeshProUGUI _passiveIncomeText;
    [SerializeField] private TextMeshProUGUI _perStepIncomeText;
    [SerializeField] private TextMeshProUGUI _catPriceText;
    [SerializeField] private TextMeshProUGUI _catUpgradeText;
    [SerializeField] private TextMeshProUGUI _stepUpgradeText;


    void Update()
    {
        _treatsText.text = "Treats: " + _currentTreats.value.ToString();
        _catsText.text = "Cats: " + _currentCats.value.ToString();
        _passiveIncomeText.text = "per second: " + _currentPassiveIncome.value.ToString();
        _perStepIncomeText.text = "per step: " + _perStepIncome.value.ToString();

        _catPriceText.text = "costs " + _catPrice.value.ToString() + " Treats";
        _catUpgradeText.text = "costs " + _catUpgradePrice.value.ToString() + " Treats";
        _stepUpgradeText.text = "costs " + _stepUpgradePrice.value.ToString() + " Treats";
    }
}
