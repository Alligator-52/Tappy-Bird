using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Coroutine moverCoroutine;
    public Action PipeStartAction, PipeStopAction;
    public void OnEnable()
    {
        PipeStartAction += StartMoving;
        PipeStopAction += StopMoving;
    }

    private void StartMoving()
    {
        StopMoving();
        moverCoroutine = StartCoroutine(MoverCoroutine());
    }

    private void StopMoving()
    {
        if (moverCoroutine == null) return;
        StopCoroutine(moverCoroutine);
        moverCoroutine = null;
    }

    private IEnumerator MoverCoroutine()
    {
        while (true)
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
            yield return null;
        }
    }
    //private void Update()
    //{
    //    transform.position += speed * Time.deltaTime * Vector3.left;
    //}

    public void OnDisable()
    {
        PipeStartAction -= StartMoving;
        PipeStopAction -= StopMoving;
    }
}
