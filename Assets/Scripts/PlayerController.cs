using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
	public Text countText;
	public Text collisonText;
	public float speed;

	private Rigidbody rb;
    private string player_id;
    private string other_id;

	private int count;
	private float fallMultiplier;
	private float lowJumpMultiplier;
	private float JumpVelocity;

    private AudioSource[] sources;
    private AudioSource shortJump, longJump, collide, cubes, bonuses;

    private bool jumpPressed;
    private bool showPopUp = false;


	void Start() {

		rb = GetComponent<Rigidbody>();
        if(this.gameObject.CompareTag("PlayerA")) 
        {
            player_id = "A";
            other_id = "B";
        }
        else
        {
            player_id = "B";
            other_id = "A";
        }

		count = 0;
		fallMultiplier = 10f;
		lowJumpMultiplier = 5f;
		jumpPressed = false;
		collisonText.text = "";
		SetCountText();

		sources = GetComponents<AudioSource>();
		shortJump = sources[0];
		longJump = sources[1];
		collide = sources[2];
		cubes = sources[4];
		bonuses = sources[3];
	}


    void FixedUpdate() {

        float moveHorizontal, moveVertical;
        string jumpKey;

        // Get Inputs for Movement
        if(player_id == "A") 
        {
        	moveHorizontal = Input.GetAxis("Horizontal");
        	moveVertical = Input.GetAxis("Vertical");
            jumpKey = "space";
        }
        else 
        {
            moveHorizontal = Input.GetAxis("Horizontal2");
            moveVertical = Input.GetAxis("Vertical2");
            jumpKey = "x";
        }
        
        // Manage jumps
        if(rb.velocity.y < 0) 
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if(rb.velocity.y > 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
		
		// Get Inputs for Jumps
        if (Input.GetKeyDown (jumpKey) && rb.transform.position.y <= 0.5f) 
        {
        	Vector3 jump;
        	// Short Jump
        	if(!jumpPressed) {
            	jump = new Vector3 (moveHorizontal, 500f, moveVertical);
            	jumpPressed = true;
            	shortJump.Play();
        	}

        	// Long Jump
            else {
            	jump = new Vector3 (moveHorizontal, 700f, moveVertical);
            	jumpPressed = false;
            	longJump.Play();
            }
            rb.AddForce (jump);
        }

        else 
        {
        	Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        	rb.AddForce(movement * speed);
        }
    }


    // Getter method for count variable
    public int getCount() {
    	return count;
    }


    // Collisions with the two kinds of Pick-up Objects
    void OnTriggerEnter(Collider other) {

    	if(other.gameObject.CompareTag("Pick Up")) 
    	{
    		other.gameObject.SetActive(false);
    		// Respawn after 10 seconds
            StartCoroutine(Respawn(other, 10f));
    		count = count + 1;
    		SetCountText();
    		cubes.Play();
    	}

        if(other.gameObject.CompareTag("Bonus")) 
        {
            other.gameObject.SetActive(false);
            // Respawn after 20 seconds
            StartCoroutine(Respawn(other, 20f));
            count = count + 2;
            SetCountText();
            bonuses.Play();
        }
    }


    // For respawning game objects
    IEnumerator Respawn(Collider other, float respawnTime) {
        yield return new WaitForSeconds(respawnTime);
        other.gameObject.SetActive(true);
    }


    // Collisions with walls and other player
    void OnCollisionEnter(Collision coll) {

        if(coll.gameObject.CompareTag("Wall")) 
        {
        	collide.Play();
            count = count - 3;
            SetCountText();
            CheckCount();
        }

        if(coll.gameObject.CompareTag("PlayerA") || coll.gameObject.CompareTag("PlayerB")) 
        {
			collide.Play();
			// Compare heights of this player and the other player
            if(this.gameObject.transform.position.y < coll.gameObject.transform.position.y) 
            {
                count = count - 1;
                SetCountText();
                CheckCount();
                collisonText.text = "Player" + other_id + " wins the Collision!";
            }

            else if(this.gameObject.transform.position.y > coll.gameObject.transform.position.y) {
            	collisonText.text = "Player" + player_id + " wins the Collision!";
            }

            else {
            	collisonText.text = "Both players win the Collision!";
            }
        }
    }


    // Popup Window
    void OnGUI() {

      if (showPopUp)
       {
         GUI.Window(0, new Rect((Screen.width/2)-150, (Screen.height/2)-75, 300, 150), ShowGUI, "Player" + other_id + " wins the game!");
     
       }
    }


    void ShowGUI(int windowID) {

        GUI.Label(new Rect(60, 40, 300, 30), "Player" + player_id + " loses due to Negative Score!");
 
        if (GUI.Button(new Rect(120, 80, 75, 30), "OK"))
        {
            showPopUp = false;
        }

        // Reset Game
         if(showPopUp == false) {
            SceneManager.LoadScene("MiniGame");
            OptionsController.PlayGame();
        }
    }
 

    void CheckCount() {
    	// End game in case of Negative score
        if(count < 0) 
        {
            collisonText.text = "";
            OptionsController.PauseGame();
            showPopUp = true;
        }
    }


    // Set the text for scores display on screen
    void SetCountText() {
    	countText.text = "Score " + player_id + ": " + count.ToString();
    }

}
