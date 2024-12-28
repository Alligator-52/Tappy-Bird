using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipeObstacle;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float destroytime = 10f;
    [SerializeField] private float maxTime = 3f;
    [SerializeField] private float pipeHeight = 10f;
    private readonly float delay = 0.005f;
    private readonly int poolSize = 20;
    private Queue<GameObject> pipes = new();
    private List<(GameObject pipe, PipeMove pipeMove, float spawnTime)> activePipes = new();

    private void Awake()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject pipe = Instantiate(pipeObstacle, transform.position, Quaternion.identity);
            pipe.SetActive(false);
            pipes.Enqueue(pipe);
        }
    }
    //private void Update()
    //{
    //    if(timer > maxTime)
    //    {
    //        GameObject newPipe = Instantiate(pipeObstacle);
    //        newPipe.transform.position = new Vector3(transform.position.x, Random.Range(pipeHeight, -pipeHeight), transform.position.z);
    //        Destroy(newPipe,destroytime);
    //        timer = 0;

    //    }
    //    timer += Time.deltaTime;
    //    maxTime -= delay * Time.deltaTime;
    //    if(maxTime < 0.8f)
    //    {
    //        maxTime = 0.8f;
    //    }
    //}

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > maxTime)
        {
            SpawnPipe();
            timer = 0;
        }

        maxTime -= delay * Time.deltaTime;
        if (maxTime < 0.8f)
        {
            maxTime = 0.8f;
        }

        RecyclePipes();
    }

    private void SpawnPipe()
    {
        if(pipes.Count > 0)
        {
            GameObject pipe = pipes.Dequeue();
            pipe.transform.position = new Vector3(transform.position.x, Random.Range(pipeHeight, -pipeHeight) ,transform.position.z);
            pipe.SetActive(true);
            var pipeMove = pipe.GetComponent<PipeMove>();
            activePipes.Add((pipe, pipeMove, Time.time));
            pipeMove.PipeStartAction?.Invoke();
        }
    }

    private void RecyclePipes()
    {
        for(int i = activePipes.Count - 1; i >= 0; i--)
        {
            (GameObject pipe, PipeMove pipeMove, float spawnTime) = activePipes[i];

            if (Time.time - spawnTime >= destroytime)
            {
                pipe.SetActive(false);
                pipeMove.PipeStopAction?.Invoke();
                pipes.Enqueue(pipe);
                activePipes.RemoveAt(i);
            }
        }
    }
}
