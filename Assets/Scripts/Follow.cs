using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    
    public GameObject objectToFollow;
    
    public float speed = 2.0f;

    private float boundary_x;
    private float boundary_y;

	private void Start()
	{
        GameObject boundaryObj = GameObject.Find("Boundaries");
        BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

        boundary_x = boundary.cameraBoundary_x;
        boundary_y = boundary.cameraBoundary_y;
    }

	void Update () {

        GameObject gameManager = GameObject.Find("Game Manager");
        
        if (gameManager.GetComponent<GameManager>().gaming == true){


            float interpolation = speed * Time.deltaTime;
            
            Vector3 position = this.transform.position;

    		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		Vector2 direction = new Vector2 (mousePos.x - objectToFollow.transform.position.x, mousePos.y - objectToFollow.transform.position.y);
    		

            Vector2 newposition = new Vector2(objectToFollow.transform.position.x, objectToFollow.transform.position.y+0.1f);

            direction.Normalize();
            newposition += direction;


            position.x = Mathf.Clamp(Mathf.Lerp(this.transform.position.x, newposition.x, interpolation),-boundary_x, boundary_x);
            position.y = Mathf.Clamp(Mathf.Lerp(this.transform.position.y, newposition.y, interpolation), -boundary_y, boundary_y);
            
            this.transform.position = position;
        }
    }
}