using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : SingletonMonoBehaviour<MoneyManager>
{
    [SerializeField] private IntEvent _onMoneyChanged;
    [SerializeField] private AudioReference _spendAudio;

    [field: SerializeField] public int Money { get; private set; } = 400;

    public void Spend(int amount)
    {
        if (Money < amount) return;
        Money -= amount;
        _onMoneyChanged?.Invoke(Money);
        AudioManager.Instance?.PlaySound(gameObject, _spendAudio);
    }

    public void Gain(int amount)
    {
        Money += amount;
        _onMoneyChanged?.Invoke(Money);
    }
}
