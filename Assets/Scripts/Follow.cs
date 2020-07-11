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
        float interpolation = speed * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        position.x = Mathf.Clamp(Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation),-boundary_x, boundary_x);
        position.y = Mathf.Clamp(Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation), -boundary_y, boundary_y);
        
        this.transform.position = position;
    }
}