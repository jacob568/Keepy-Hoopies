using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneAi : MonoBehaviour
{
    public Transform ball;
    private float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(ball.position.x, 0f, ball.position.z)) > 1f)
            {
                transform.LookAt(ball);
                gameObject.transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 90f, 0f);
                transform.position += -transform.right * speed * Time.deltaTime;
            }
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        collision.rigidbody.AddExplosionForce(40f, transform.position, 10f);
    }
}
