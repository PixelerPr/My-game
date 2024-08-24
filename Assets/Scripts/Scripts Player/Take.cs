using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take : MonoBehaviour
{
    public GameObject RUKA; // ������ �� ������, �������������� ���� ������
    private FPS nf; // ������ �� ��������� New_FPS ������
    public GameObject player; // ������ �� ������, �������������� ������
    private New_Interact inter; // ������ �� ��������� New_Interact �������� �������
    public string name_item;
    [SerializeField] private LayerMask mask;

    
    private void Start()
    {
        nf = player.GetComponent<FPS>(); // �������� ��������� New_FPS ������
        inter = GetComponent<New_Interact>(); // �������� ��������� New_Interact �������� �������
    }

    // ����� ��� �������� ��������
    public void Pick_up()
    {
        // ���������, �������� �� ���� ������
        if (nf.handEmpty)
        {
            // ���������, ��������� �� ������ ���� ��� ��������
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ������� ��� �� ������� ����
            RaycastHit hit; // ���������� ��� �������� ���������� � ����������� ���� � �����������
            if (Physics.Raycast(ray, out hit, mask)) // ��������� ����������� ���� � �����������
            {
                Debug.DrawRay(ray.origin, ray.direction * 2f, Color.yellow);
				Debug.Log("1:" + hit.GetType() + "2:" + hit.distance + "3:" + hit.collider);
				Debug.Log("MASSSSSK");
                // ������������� ������� ������ ��� �������� ��� �������, � ������� ��������� ���
                hit.collider.transform.parent = RUKA.transform;

                // ��������� ������ �������
                hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                hit.collider.GetComponent<Rigidbody>().useGravity = false;

                // ��������� ��������� �������
                hit.collider.GetComponent<BoxCollider>().enabled = false;

                // ������������� ������ �� ���� ������
                hit.collider.transform.position = RUKA.GetComponent<Transform>().position;
                hit.collider.transform.rotation = RUKA.GetComponent<Transform>().rotation;

                // ���������, ��� ������ � ������� ��������� ��� ��� ������� ������
                if (hit.collider.gameObject == gameObject)
                {
                    // ��������� �������
                    nf.handEmpty = false;
                    nf.name_item_inHands = name_item;
                }
            }
        }
    }

    void Update()
    {
        // ���� ����� ����� ������ ��� ������������ ��������
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (RUKA.transform.childCount > 0) // ���������, ���� �� ������ � ���� ������
            {
                // �������� ���������� Rigidbody � BoxCollider ������������ �������
                Rigidbody itemRigidbody = RUKA.transform.GetChild(0).GetComponent<Rigidbody>();
                BoxCollider itemCollider = RUKA.transform.GetChild(0).GetComponent<BoxCollider>();

                // �������� ������ �������
                itemRigidbody.isKinematic = false;
                itemRigidbody.useGravity = true;
                itemCollider.enabled = true;
                nf.handEmpty = true;
                nf.name_item_inHands = null;

                // ����������� ������ �� ���� ������
                RUKA.transform.GetChild(0).parent = null;
            }
        }

    }
}