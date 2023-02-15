using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Slider
{
    public IntEvent HealthChangedEvent;
    private Animator _animator;
    private float _maxHeartbeatSpeed = 1.5f;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        HealthChangedEvent?.RegisterAction(OnHealthChanged);
    }

    private void OnHealthChanged(int health)
    {
        value = health;
        _animator.speed = CalculateHeartBeatSpeed();
        _animator.SetTrigger("HeartbeatHit");
    }

    private float CalculateHeartBeatSpeed()
    {
        // Calculate the range of the input value
        float range = maxValue - minValue;

        // Calculate the percentage of the input value within the range
        float valuePercentage = (value - minValue) / range;

        // Calculate the speed based on the input value percentage
        float speed = Mathf.Lerp(_maxHeartbeatSpeed, 1, valuePercentage);

        return speed;
    }
}
