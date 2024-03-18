using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumkinHp : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue; // ����ü��/�ִ�ü�� ������ ü�¹� ����
    }

    public void ActiveHealthBar(bool active)
    {
        gameObject.SetActive(active); // active ���¿� ���� ü�¹� Ȱ��ȭ ���� ����
    }
}
