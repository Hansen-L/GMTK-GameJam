using UnityEngine;

public class Boundaries : MonoBehaviour 
{
	public float cameraBoundary_x = 3.87f;
	public float cameraBoundary_y = 4.86f;

	public float playerBoundary_x = 14.83f;
	public float playerBoundary_y = 9.04f;

    private float boundary_x;
    private float boundary_y;

    //private void Start() // THIS DOESN'T WORK LOL HARD CODED
    //{
    //    GameObject boundaryObj = GameObject.Find("Boundaries");
    //    Boundaries boundary = boundaryObj.GetComponent<Boundaries>();

    //    float x = boundary.playerBoundary_x;
    //    float y = boundary.playerBoundary_y;

    //    EdgeCollider2D collider = this.GetComponent<EdgeCollider2D>();

    //    collider.points[0] = new Vector2(x, y);
    //    collider.points[1] = new Vector2(-x, y);
    //    collider.points[2] = new Vector2(-x, -y);
    //    collider.points[3] = new Vector2(-x, y);
    //    collider.points[4] = new Vector2(x, y);
    //}
}
