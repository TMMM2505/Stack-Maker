using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckOwnMap : MonoBehaviour
{
    public Image imgTick;
    public Image imgLock;

    public void SetTick()
    {
        imgTick.gameObject.SetActive(true);
        imgLock.gameObject.SetActive(false);
    }
    public void SetLock()
    {
        imgTick.gameObject.SetActive(false);
        imgLock.gameObject.SetActive(true);
    }
}
