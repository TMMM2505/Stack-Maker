using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
    public void OnStart()
    {
        offset = new Vector3(0, 10, -15);
        transform.rotation = Quaternion.Euler(new Vector3(35f, 0, 0));
    }
    public void OnFinish()
    {
        offset = new Vector3(10, 8, -20);
        transform.DOMove((target.position + offset), 0.1f).SetEase(Ease.Linear);
        transform.DORotate(new Vector3(8f, 338f, 358f), 0.5f, RotateMode.Fast);
    }
}
