using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
    [SerializeField] private IntVariable _catPriceRef;
    [SerializeField] private IntVariable _catUpgradePriceRef;
    [SerializeField] private IntVariable _stepUpgradePriceRef;

    private int _catPrice = 20;
    private int _catUpgradePrice = 500;
    private int _stepUpgradePrice = 200;

    private int _catPriceIncrement = 2;
    private int _catUpgradePriceIncrement = 4;
    private int _stepPriceIncrement = 2;

    private const string CAT_PRICE = "CatPrice";
    private const string CAT_UPGRADE_PRICE = "CatUpgradePrice";
    private const string STEP_UPGRADE_PRICE = "StepUpgradePrice";

    // Start is called before the first frame update
    void Start()
    {
        GetPlayerPrefsAndSetRefs();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            GetPlayerPrefsAndSetRefs();
        }
        else
        {
            SetPlayerPrefs();
        }
    }

    private void GetPlayerPrefsAndSetRefs()
    {
        if (PlayerPrefs.HasKey(CAT_PRICE))
            _catPrice = PlayerPrefs.GetInt(CAT_PRICE);

        if (PlayerPrefs.HasKey(CAT_UPGRADE_PRICE))
            _catUpgradePrice = PlayerPrefs.GetInt(CAT_UPGRADE_PRICE);

        if (PlayerPrefs.HasKey(STEP_UPGRADE_PRICE))
            _stepUpgradePrice = PlayerPrefs.GetInt(STEP_UPGRADE_PRICE);

        SetRefs();
    }

    private void SetRefs()
    {
        _catPriceRef.value = _catPrice;
        _catUpgradePriceRef.value = _catUpgradePrice;
        _stepUpgradePriceRef.value = _stepUpgradePrice;
    }

    private void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt(CAT_PRICE, _catPrice);
        PlayerPrefs.SetInt(CAT_UPGRADE_PRICE, _catUpgradePrice);
        PlayerPrefs.SetInt(STEP_UPGRADE_PRICE, _stepUpgradePrice);
        PlayerPrefs.Save();
    }

    public int GetCatPrice()
    {
        return _catPrice;
    }
    public int GetCatUpgradePrice()
    {
        return _catUpgradePrice;
    }
    public int GetStepUpgradePrice()
    {
        return _stepUpgradePrice;
    }

    public void IncreasePriceOfCat()
    {
        MoneyManagerHandle.Instance.ReduceBy(_catPrice);

        _catPrice *= _catPriceIncrement;
        SetPlayerPrefs();
        SetRefs();
    }
    public void IncreasePriceOfUpgradeCat()
    {
        MoneyManagerHandle.Instance.ReduceBy(_catUpgradePrice);

        _catUpgradePrice *= _catUpgradePriceIncrement;
        SetPlayerPrefs();
        SetRefs();
    }
    public void IncreasePriceOfUpgradeSteps()
    {
        if (MoneyManagerHandle.Instance.GetCurrentTreats() >= _stepUpgradePrice)
        {
            MoneyManagerHandle.Instance.ReduceBy(_stepUpgradePrice);
            MoneyManagerHandle.Instance.IncreaseMulitplier();

            _stepUpgradePrice *= _stepPriceIncrement;
            SetPlayerPrefs();
            SetRefs();
        }
    }
}
