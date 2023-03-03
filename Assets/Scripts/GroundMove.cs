using UnityEngine;

public class GroundMove : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private double nextPos = 8.44;
    [SerializeField] private double resetPos = -7.04;
    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime; 
        if(transform.position.x < resetPos)
        {
            transform.position = new Vector2((float)nextPos, transform.position.y);
        }
    }
}
