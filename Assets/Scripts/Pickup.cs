using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory= GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();//�ҵ���ҵı�ǩ��������
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//�Աȱ�ǩ
        {
            for(int i = 0; i < inventory.slots.Length; i++)//������
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;//ʰȡ���ߺ��ж�Ϊ��ʰȡ
                    Destroy(gameObject);//ʰȡ�����ٵ�ͼ�еĵ���
                    Instantiate(itemButton, inventory.slots[i].transform, false);//���ɰ�ť��ͼƬλ��                   
                    break;
                }              
            }
        }
    }
}
