using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public Transform handTransform; // ������ �� ��������� ���� ������

    void Update()
    {
        // ���������, ���� �� ������ � ���� ������
        if (handTransform.childCount > 0)
        {
            // �������� ������, ������� ��������� � ���� ������
            Transform heldItem = handTransform.GetChild(0);

            // ������������� ���������� ���������� � ������� ������� � ���� ������
            heldItem.localPosition = Vector3.zero; // �������� ��������� �������
            heldItem.localRotation = Quaternion.identity; // �������� ��������� ����������

            // ����� ����� ���������� ������ �������� ��� ���������� � ������� � ������������ � ������������
            // ��������, ����� ������ ������� ������, ����� ���������� ��� ��������� ����������:
             heldItem.localRotation = Quaternion.Euler(90f, 0f, 0f); // ��������, ������ ������� ������
            // ����� ����� ����������� ������� ������� � ����, ����� �� �������� �����������

            // ����� ����� �������� �������������� �������, ����� ��� ��������, ������� �������� ���������� � ������� ������� � ����
        }
    }
}