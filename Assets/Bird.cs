
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{

    private Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingArround;

    [SerializeField] private float _launchPower = 350;

    
    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);

        // check the bird was stopped or really slow?
        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            // How many time passed 
            _timeSittingArround += Time.deltaTime;
        }

        // if y value is greater than 10 then Scene will be reloaded 
      if (transform.position.y > 150 ||
             transform.position.y < -150 ||
             transform.position.x > 150 ||
             transform.position.x < -150 || 
             _timeSittingArround > 3)        
         {
             string currentSceneName = SceneManager.GetActiveScene().name;
             SceneManager.LoadScene(currentSceneName);
         }

       
    }

    // OnMouseDown is called when user pressed the mouse 
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        // lauch the bird. multiple force with values to make this force more powerful
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        // add gravity to rigidbody
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;
        GetComponent<LineRenderer>().enabled = false;


    }

    private void OnMouseDrag()
    {
        // Connect to World Position
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x,newPosition.y);
    }
}
