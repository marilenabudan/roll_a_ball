using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;

    public GameObject theEndUI;
    public GameObject startUI;

    private Rigidbody rb;
    private int count;
    private bool gameOn;

    void Start ()
    {
      rb = GetComponent<Rigidbody>();

      count = 0;
      gameOn = false;

      SetCountText();

      theEndUI.SetActive(false);
      startUI.SetActive(true);
    }

    void FixedUpdate()
    {
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");

      if (gameOn == true) {
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement*speed);

        if (rb.position.y < -1f){
          FindObjectOfType<GameManager>().Restart();
          startUI.SetActive(false);
        }
      }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Pick up gold")){
        other.gameObject.SetActive(false);
        count = count + 5;
        SetCountText();
      }
      if (other.gameObject.CompareTag("Pick up rose")){
        other.gameObject.SetActive(false);
        count = count + 3;
        SetCountText();
      }
      if (other.gameObject.CompareTag("Pick up blue")){
        other.gameObject.SetActive(false);
        count = count + 1;
        SetCountText();
      }
    }

    void SetCountText ()
    {
      countText.text = count.ToString();
      if (count >= 36){
        theEndUI.SetActive(true);
      }
    }

    public void StartGame(){
      startUI.SetActive(false);
      gameOn = true;
    }
}
