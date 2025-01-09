using UnityEngine;

public class Cat
{
    private int _level = 1;
    private float _multiplier = 0.25f;

    public Cat(int level)
    {
        _level = level;
    }

    public void Update()
    {
        MoneyManagerHandle.Instance.IncreaseBy(_level * _multiplier);
    }

    public void LevelUp()
    {
        _level++;
    }

    public float GetIncreasePerCat() { return _multiplier * _level; }
}
