using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class Cats : MonoBehaviour
{
    [SerializeField] private IntVariable _currentCats;
    [SerializeField] private FloatVariable _perSecondIncome;
    [SerializeField] private IntVariable _lastDateTime;
    [SerializeField] private GameObject _spawnZone;
    [SerializeField] private List<GameObject> _catObjects = new List<GameObject>();

    public int _catsLevel = 1;
    private List<Cat> _cats = new List<Cat>();
    private int _interval = 1;
    private MeshCollider _spawnZoneCollider;
    private int _catCount = 0;

    private const string CURRENT_CAT_COUNT = "CurrentCatsCount";
    private const string CURRENT_CATS_LEVEL = "CurrentCatsLevel";

    private void Start()
    {
        _spawnZoneCollider = _spawnZone.GetComponent<MeshCollider>();


        if (PlayerPrefs.HasKey(CURRENT_CAT_COUNT))
            _catCount = PlayerPrefs.GetInt(CURRENT_CAT_COUNT);

        if (PlayerPrefs.HasKey(CURRENT_CATS_LEVEL))
            _catsLevel = PlayerPrefs.GetInt(CURRENT_CATS_LEVEL);

        _currentCats.value = _catCount;

        for (int i = 0; i < _catCount; i++)
        {
            _cats.Add(new Cat(_catsLevel));
            SpawnNewCat();
        }
        UpdateTreatsIncrease();
    }

    private void UpdateTreatsIncrease()
    {
        RecalculatePerSecondIncome();
        if (TimeManagerHandle.Instance.passedSeconds != 0)
        {
            float treatsToAdd = 0;

            foreach (var cat in _cats)
            {
                treatsToAdd += cat.GetIncreasePerCat() * TimeManagerHandle.Instance.passedSeconds;
            }
            MoneyManagerHandle.Instance.IncreaseBy(treatsToAdd);
        }
    }

    void Update()
    {
        if (Time.time >= _interval)
        {
            _interval = Mathf.FloorToInt(Time.time) + 1;

            foreach (Cat cat in _cats)
            {
                cat.Update();
            }
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (PlayerPrefs.HasKey(CURRENT_CAT_COUNT))
            {
                _catCount = PlayerPrefs.GetInt(CURRENT_CAT_COUNT);
                _catsLevel = PlayerPrefs.GetInt(CURRENT_CATS_LEVEL);
                UpdateTreatsIncrease();
            }
        }
        else
        {
            PlayerPrefs.SetInt(CURRENT_CAT_COUNT, _catCount);
            PlayerPrefs.SetInt(CURRENT_CATS_LEVEL, _catsLevel);
            PlayerPrefs.Save();
        }
    }

    public void BuyCat()
    {
        if (MoneyManagerHandle.Instance.GetCurrentTreats() >= PriceManagerHandle.Instance.GetCatPrice())
        {
            PriceManagerHandle.Instance.IncreasePriceOfCat();


            SpawnNewCat();

            _cats.Add(new Cat(_catsLevel));
            _catCount++;
            PlayerPrefs.SetInt(CURRENT_CAT_COUNT, _catCount);
            PlayerPrefs.Save();
            RecalculatePerSecondIncome();
        }
    }
    public void LevelUpCats()
    {
        if (MoneyManagerHandle.Instance.GetCurrentTreats() >= PriceManagerHandle.Instance.GetCatUpgradePrice())
        {
            foreach (var cat in _cats)
            {
                cat.LevelUp();
                _catsLevel++;

                PlayerPrefs.SetInt(CURRENT_CATS_LEVEL, _catsLevel);
                PlayerPrefs.Save();
            }
            PriceManagerHandle.Instance.IncreasePriceOfUpgradeCat();
            RecalculatePerSecondIncome();
        }
    }

    private void SpawnNewCat()
    {
        // source: https://www.youtube.com/watch?v=4OQjnKUENoE
        float screenX = UnityEngine.Random.Range(_spawnZoneCollider.bounds.min.x, _spawnZoneCollider.bounds.max.x);
        float screenY = UnityEngine.Random.Range(_spawnZoneCollider.bounds.min.y, _spawnZoneCollider.bounds.max.y);
        Vector2 pos = new Vector2(screenX, screenY);
        int randomCat = UnityEngine.Random.Range(0, _catObjects.Count);
        GameObject randomCatToSpawn = _catObjects[randomCat];

        Instantiate(randomCatToSpawn, pos, randomCatToSpawn.transform.rotation);
    }

    private void RecalculatePerSecondIncome()
    {
        float newValue = 0;
        foreach (var cat in _cats)
        {
            newValue += cat.GetIncreasePerCat();
        }
        _perSecondIncome.value = newValue;
        _currentCats.value = _catCount;
    }
}
