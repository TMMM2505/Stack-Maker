using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Action winAction;
    public Action gotoGoal;

    [SerializeField] private GameObject body;
    [SerializeField] private LayerMask layerBrick;
    [SerializeField] private LayerMask layerPath;
    [SerializeField] private float speed;
    [SerializeField] private Animator anim;

    private string currentAnim;
    private Stack<Brick> bricks = new();
    
    private Vector3 direction;
    private RaycastHit hit;
    private Vector3 target;

    private bool onStartMoving = true;
    private bool isMoving, changeDirection;
    private bool holdMouse;

    private float coin;


    private Vector3 lastMousePosition;
    private Vector3 swipeDirection;
    private bool isDragging = false, canControl = true;
    private float angle;

    private void Awake()
    {
        coin = 1000;
    }
    public void OnInit(Vector3 startPoint)
    {
        transform.position = startPoint;
        ChangeAnim("idle");
    }
    private void Update()
    {
        HandleInput();
        Move();
    }

    private void HandleInput()
    {
        if (!isMoving)
        {
            if (onStartMoving)
            {
                ChangeAnim("idle");
                onStartMoving = false;
            }
            

            if (canControl)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lastMousePosition = Input.mousePosition;
                    isDragging = true;
                }
                if (Input.GetMouseButton(0) && isDragging)
                {
                    Vector3 currentMousePosition = Input.mousePosition;
                    swipeDirection = currentMousePosition - lastMousePosition;
                    lastMousePosition = currentMousePosition;
                    if (swipeDirection.magnitude > 0)
                    {
                        angle = Vector3.Angle(swipeDirection, new Vector3(1, 0, 0));
                        if (-45 < angle && angle < 45) TryMove(Vector3.right);
                        else if (45 < angle && angle < 135 && swipeDirection.y >= 0) TryMove(Vector3.forward);
                        else if (45 < angle && angle < 135 && swipeDirection.y < 0) TryMove(Vector3.back);
                        else if (135 < angle && angle < 225) TryMove(Vector3.left);
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isDragging = false;
                    swipeDirection = Vector3.zero;
                }
            }
            
        }
    }

    private void TryMove(Vector3 direction)
    {
        Vector3 rayOrigin = transform.position + new Vector3(0, 1f, 0) + direction;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 10f, layerPath))
        {
            this.direction = direction;
            isMoving = true;
            //target = hit.point + new Vector3(0, 1f, 0);
            target = transform.position + direction;
            changeDirection = false;
        }
    }

    private void Move()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            FindPath();
            if (!Physics.Raycast(transform.position + direction * 2 / 3 + new Vector3(0, 1f, 0), Vector3.down, out hit, 10f, layerPath))
            {
                isMoving = false;
                transform.position = transform.position + direction * 1 / 4;
            }
            else if (hit.collider != null)
            {
                if (bricks.Count == 0)
                {
                    if(Physics.Raycast(transform.position + direction * 2 / 3 + new Vector3(0, 1f, 0), Vector3.down, out hit, 10f, layerPath))
                    {
                        if(hit.collider.CompareTag(Cache.keyRemovePath))
                        {
                            isMoving = false;
                            ChangeAnim("jump");
                        }
                        else
                        {
                            isMoving = true;
                            ChangeAnim("idle");
                        }
                    }
                }
                target = transform.position + direction;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(Cache.keyPush))
        {
            Push push = collision.transform.GetComponent<Push>();
            ContactPoint point = collision.contacts[0];

            transform.position = push.transform.position;
            if (89 <= Vector3.Angle(push.GetDirection2(), point.normal) && Vector3.Angle(push.GetDirection2(), point.normal) <= 91)
            {
                ChangeDirection(push.GetDirection2());
            }
            else if (89 <= Vector3.Angle(push.GetDirection1(), point.normal) && Vector3.Angle(push.GetDirection1(), point.normal) <= 91)
            {
                ChangeDirection(push.GetDirection1());
            }
        }

        if(collision.gameObject.CompareTag(Cache.keyGoal))
        {
            OnGoal();
            isMoving = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Cache.keyMarkGotoFinish))
        {
            gotoGoal?.Invoke();
        }
        
    }
    public void ChangeDirection(Vector3 newDir)
    {
        isMoving = true;
        changeDirection = true;
        TryMove(newDir);
    }

    public void FindPath()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + new Vector3(0, 3f, 0), transform.TransformDirection(Vector3.down), out hit, 10f, layerPath))
        {
            if (hit.collider.CompareTag(Cache.keyRemovePath))
            {
                if (!Physics.Raycast(transform.position + new Vector3(0, 3f, 0), transform.TransformDirection(Vector3.down), 10f, layerBrick))
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    if(!hit.collider.CompareTag(Cache.keyFinishedBrick))
                    {
                        RemoveBrickFromStack(hit.collider.gameObject);
                    }

                }
            }
            else
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 3f, 0), transform.TransformDirection(Vector3.down), out hit, 10f, layerBrick))
                {
                    Brick brick = hit.collider.gameObject.GetComponent<Brick>();
                    if (brick != null)
                    {
                        AddBrickToStack(brick);
                    }
                }
            }
        }
        
    }

    public void AddBrickToStack(Brick brick)
    {
        if (brick && !bricks.Contains(brick))
        {
            bricks.Push(brick);
            body.transform.position += new Vector3(0, 0.2f, 0);
            brick.transform.position = body.transform.position - new Vector3(0, 0.1f, 0);
            brick.transform.SetParent(transform);
            brick.gameObject.layer = 0;
        }
    }
    public void RemoveBrickFromStack(GameObject path)
    {
        if (bricks.Count > 0)
        {
            Brick brick = bricks.Pop();
            brick.transform.position = path.transform.position + new Vector3(0, 0.3f, 0);
            brick.gameObject.layer = 8;
            brick.tag = Cache.keyFinishedBrick;
            brick.transform.SetParent(path.transform);

            body.transform.position -= new Vector3(0, 0.2f, 0);
        }
        else
        {
            isMoving = false;
        }
    }
    
    private void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    public void OnGoal()
    {
        ChangeAnim("win");
        winAction?.Invoke();
        foreach (Brick brick in bricks)
        {
            if(brick && brick.gameObject)
            {
                Destroy(brick.gameObject);
                body.transform.position -= new Vector3(0, 0.2f, 0);
            }
        }
    }

    public void AddCoin(float addCoin)
    {
        coin += addCoin;
    }
    public float GetCoin() { return coin; }
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }
    public void ResetPlayer()
    {
        foreach (Brick brick in bricks)
        {
            if(brick.gameObject)
            {
                body.transform.position -= new Vector3(0, 0.2f, 0);
                Destroy (brick.gameObject);
            }
        }
        bricks.Clear();
    }
    public void DeactiveMoving()
    {
        canControl = false;
    }
    public void ActiveMoving()
    {
        canControl = true;
    }
}
