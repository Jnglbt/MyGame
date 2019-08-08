using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private Quaternion targetRotation;
    private Vector2 touchOrigin = -Vector2.one;

    float timer;
    Vector2 movement;
    Vector3 mousePosition;
    float offset = -90;

    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public GameObject bullet;
    public float speed;
    public UnityArmatureComponent player;
    public float restartLevelDelay = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        int horizontal = 0;
        int vertical = 0;
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        horizontal = (int) Input.GetAxisRaw("Horizontal");
        vertical = (int) Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(horizontal, vertical);

        // Turn the player to face the mouse cursor.
        Turning();

        // Animate the player.
        Animating(horizontal, vertical);

        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetMouseButton(0) && timer >= timeBetweenBullets)
        {
            player.animation.Play("Shoot");
            // ... shoot the gun.
            Shoot();
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;

                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;

                //Check if the difference along the x axis is greater than the difference along the y axis.
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                    horizontal = x > 0 ? 1 : -1;
                else
                    //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                    vertical = y > 0 ? 1 : -1;
            }
        }

#endif
        }

    void Move(int h, int v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed;

        // Move the player to it's current position plus the movement.
        rb.AddForce(movement);
    }

    void Turning()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

        void Shoot()
    {
        timer = 0f;
        GameObject b = (GameObject)(Instantiate(bullet, transform.position + transform.up * 1.5f, Quaternion.identity));
        b.GetComponent<Rigidbody2D>().AddForce(transform.up * 1500);
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        player.animation.Play("Run");
    }

    private void Restart()
    {
        //Load the last scene loaded, in this case Main, the only scene in the game.
        SceneManager.LoadScene("GameScene");
    }
}
