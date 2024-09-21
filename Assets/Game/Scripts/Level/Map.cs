using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<Congrats> congrats = new();
    [SerializeField] private StartPoint startPoint;
    [SerializeField] private Chest chest;
    
    public Vector3 GetStartPoint() => startPoint.transform.position + new Vector3(0,0.3f,0);

    public void OnInit()
    {
        SetOffCongrats();
        chest.Close();
    }    

    public void OnFinish()
    {
        SetOnCongrats();
        chest.Open();
    }

    private void SetOffCongrats()
    {
        for(int i = 0; i < congrats.Count; i++)
        {
            congrats[i].OnInit();
        }
    }
    private void SetOnCongrats()
    {
        for (int i = 0; i < congrats.Count; i++)
        {
            congrats[i].TurnOnVFX();
        }
    }
}
