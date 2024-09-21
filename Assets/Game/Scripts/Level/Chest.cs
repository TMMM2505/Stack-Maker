using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject openState;
    [SerializeField] private GameObject closeState;
    private void Start()
    {
        Close();
    }
    public void Open()
    {
        openState.SetActive(true);
        closeState.SetActive(false);
    }
    public void Close()
    {
        openState.SetActive(false);
        closeState.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Cache.keyPlayer))
        {
            Open();
        }
    }
}
