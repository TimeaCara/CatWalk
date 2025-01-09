using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private IntVariable _currentMoney;
    [SerializeField] private IntVariable _stepMultiplier;

    private float _money = 0;
    private int _multiplier = 1;

    private const string CURRENT_MONEY_COUNT = "CurrentMoneyCount";
    private const string CURRENT_MULTIPLIER = "CurrentMultiplier";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(CURRENT_MONEY_COUNT))
            _money = PlayerPrefs.GetFloat(CURRENT_MONEY_COUNT);

        if (PlayerPrefs.HasKey(CURRENT_MULTIPLIER))
            _multiplier = PlayerPrefs.GetInt(CURRENT_MULTIPLIER);

        _currentMoney.value = Mathf.RoundToInt(_money);
        _stepMultiplier.value = _multiplier;
    }

    public void ReduceBy(float value)
    {
        _money -= value;
        _currentMoney.value = (int)_money;

        SavePlayerPref();
    }

    public void IncreaseBy(float value, bool fromSteps = false)
    {
        if (fromSteps)
        {
            _money += value * _multiplier;
        }
        else
        {
            _money += value;
        }

        _currentMoney.value = Mathf.RoundToInt(_money);
        SavePlayerPref();
    }

    public int GetCurrentTreats()
    {
        return _currentMoney.value;
    }

    public void IncreaseMulitplier()
    {
        _multiplier += 3;
        _stepMultiplier.value = _multiplier;

        SavePlayerPref();
    }

    private void SavePlayerPref()
    {
        PlayerPrefs.SetFloat(CURRENT_MONEY_COUNT, _money);
        PlayerPrefs.SetInt(CURRENT_MULTIPLIER, _multiplier);
        PlayerPrefs.Save();
    }
}
