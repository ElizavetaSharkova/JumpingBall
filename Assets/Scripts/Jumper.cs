using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jumper : MonoBehaviour
{
    float directionX;
    float g;
    float hmax;
    string platformStr = "platform";
    string floorStr = "Floor";
    Rigidbody2D rb;
    UIManager uiManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hmax = gameObject.transform.position.y;
        g = 9.8f;
        uiManager = FindObjectOfType<UIManager>();
    }

    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (!uiManager.IsActive())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uiManager.OnPause();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                OnMouseButtonClick();
            }
            rb.velocity = new Vector2(directionX * 2f, rb.velocity.y);
            rb.freezeRotation = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.tag == platformStr)
        {
            uiManager.IncBallHit();

            float speed = Mathf.Sqrt(2 * hmax * g);
            rb.velocity = new Vector2(rb.velocity.x, speed);

            if (otherObj.name != floorStr)
            {
                UIManager.ChangePlatformColor(otherObj);
            }
        }
    }

    private void OnMouseButtonClick()
    {
        directionX = (Input.mousePosition.x > Camera.main.pixelWidth / 2) ? 1 : -1;

        Vector3 clickPosition = Input.mousePosition;
        GameObject hitObject = GetHitGameObject(clickPosition);

        if (hitObject != null)
        {
            if (hitObject.tag == platformStr && hitObject.name != floorStr)
            {
                UIManager.ChangePlatformColor(hitObject);
            }
        }
    }

    private GameObject GetHitGameObject(Vector3 clickPosition)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(clickPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        Collider2D collider = hit.collider;
        return (collider) ? hit.collider.gameObject : null;
    }

    public void OnChangeGravity(float gravity)
    {
        hmax = g / gravity * hmax;
        g = gravity;
        Physics2D.gravity = new Vector2(0, -g);
    }
}
