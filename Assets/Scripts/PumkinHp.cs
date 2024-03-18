using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumkinHp : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue; // 현재체력/최대체력 비율로 체력바 조정
    }

    public void ActiveHealthBar(bool active)
    {
        gameObject.SetActive(active); // active 상태에 따라 체력바 활성화 여부 조정
    }
}
