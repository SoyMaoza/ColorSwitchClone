using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] private Color yellowColor;
    [SerializeField] private Color purpleColor;
    [SerializeField] private Color blueColor;
    [SerializeField] private Color pinkColor;
    [SerializeField] private float restartDelay;
    [SerializeField] private ParticleSystem playerparticles;
    private string currentColor;

    [SerializeField] private float verticalForce = 400f;
    Rigidbody2D playerRb;
    SpriteRenderer playerSr;


    // Start is called before the first frame update
    void Start()
    {

        //Cambios en la propiedad Rigidbody
        playerRb = GetComponent<Rigidbody2D>();


        //Cambios en la propiedad SpriteRenderer
        playerSr = GetComponent<SpriteRenderer>();

        ChangeColor();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            playerRb.AddForce(new Vector2(0, verticalForce));
            playerRb.velocity = Vector2.zero;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColorChanger"))
        {
            ChangeColor();
            Destroy(collision.gameObject);
            return;
        }
        if (collision.gameObject.tag != currentColor)
        {
            gameObject.SetActive(false);
            Instantiate(playerparticles, transform.position, Quaternion.identity);
            Invoke("RestartScene", restartDelay);


        }
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            gameObject.SetActive(false);
            Instantiate(playerparticles, transform.position, Quaternion.identity);
            Invoke("LoadNextScene", restartDelay);
            return;
        }
    }

    void RestartScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
        
    }

    void ChangeColor()
    {

        {
            int randomNumber = Random.Range(0, 4);
            Debug.Log(randomNumber);
            if (randomNumber == 0)
            {
                playerSr.color = yellowColor;
                currentColor = "Yellow";
            }
            else if (randomNumber == 1)
            {
                playerSr.color = purpleColor;
                currentColor = "Purple";
            }
            else if (randomNumber == 2)
            {
                playerSr.color = blueColor;
                currentColor = "Blue";
            }
            else if (randomNumber == 3)
            {
                playerSr.color = pinkColor;
                currentColor = "Pink";
            }
        }
    }
    void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1);
    }
}