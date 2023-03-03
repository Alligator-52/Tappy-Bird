using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipeObstacle;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float destroytime = 10f;
    [SerializeField] private float maxTime = 3f;
    [SerializeField] private float pipeHeight = 10f;
    private readonly float delay = 0.005f;

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
}
