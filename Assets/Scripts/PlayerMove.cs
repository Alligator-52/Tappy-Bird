using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private bool isPressed = false;
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private float upForce = 100;
    [SerializeField] Animator playerAnimator;
    //[SerializeField] private GameObject pausePanel;
    [Header("Audio")]
    public AudioSource tapSound;
    public AudioSource deathSound;
    public AudioSource scoreSound;

    [Header("Ads")]
    [SerializeField] private InterstitialAd interstitialAd;

    private GameManager gameManager;

    [HideInInspector]
    public int deathCount = 0;
    private void Start()
    {
        deathCount = PlayerPrefs.GetInt("DeathCount");
        gameManager = FindFirstObjectByType<GameManager>();
        //Debug.Log(deathCount);
    }
    void Update()
    {
        if (gameManager.GameState != Support.PlayState.Playing) return;
        if ((IsPointerOverPauseButton()))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            isPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }
    }

    private bool IsPointerOverPauseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var raycastResults = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            foreach (var result in raycastResults)
            {
                if (result.gameObject.CompareTag("pause"))
                {
                    return true;
                }
            }
        }

        return false;
    }
    private void FixedUpdate()
    {
        if(isPressed)
        {
            player.linearVelocity = Vector2.up * upForce;
            tapSound.Play();
            isPressed = false;
        }
        var localVel = transform.InverseTransformDirection(player.linearVelocity);
        if(localVel.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 30);
        }
        if(localVel.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -30);
        }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("score"))
        {
            //Debug.Log("Score!");
            scoreSound.Play();
            gameManager.CountScore();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            deathCount+=1;
            PlayerPrefs.SetInt("DeathCount", deathCount);
            if (deathCount > 4)
            {
                deathCount = 0;
                PlayerPrefs.SetInt("DeathCount", deathCount);
                interstitialAd.ShowAd();
            }
            //Debug.Log("Death Count = " + deathCount);
            //Debug.Log("Die!");
            playerAnimator.SetBool("isDead", true);
            deathSound.Play();
            gameManager.GameOver();

        }
    }
}
