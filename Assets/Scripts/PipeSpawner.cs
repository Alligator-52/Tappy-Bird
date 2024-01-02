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

    private List<GameObject> pipes = new List<GameObject>();

    void Update()
    {
        if(timer > maxTime)
        {
            GameObject newPipe = Instantiate(pipeObstacle);
            newPipe.transform.position = new Vector3(transform.position.x, Random.Range(pipeHeight, -pipeHeight), transform.position.z);
            Destroy(newPipe,destroytime);
            timer = 0;

        }
        timer += Time.deltaTime;
        maxTime -= delay * Time.deltaTime;
        if(maxTime < 0.8f)
        {
            maxTime = 0.8f;
        }

    }

    /*private void InstantiatePipes()
    {
        GameObject newPipe = Instantiate(pipeObstacle);
        newPipe.transform.position = new Vector3(transform.position.x, Random.Range(pipeHeight, -pipeHeight), transform.position.z);
        newPipe.SetActive(false);
        pipes.Add(newPipe);
    }

    private void ResetPipe(GameObject pipe,Vector3 position)
    {
        Debug.Log("Reset called");
        if(timer > destroytime)
        {
            pipe.SetActive(false);
            pipe.transform.position = position;
        }
    }*/
}
