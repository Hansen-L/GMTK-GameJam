using UnityEngine;

public class StageBoundary : MonoBehaviour 
{
    private float boundary_x;
    private float boundary_y;

    private void Start() // THIS DOESN'T WORK LOL HARD CODED
    {
        GameObject boundaryObj = GameObject.Find("Boundaries");
        BoundaryNumbers boundary = boundaryObj.GetComponent<BoundaryNumbers>();

        float x = boundary.playerBoundary_x;
        float y = boundary.playerBoundary_y;

        EdgeCollider2D collider = this.GetComponent<EdgeCollider2D>();

        collider.points[0] = new Vector2(x, y);
        collider.points[1] = new Vector2(-x, y);
        collider.points[2] = new Vector2(-x, -y);
        collider.points[3] = new Vector2(-x, y);
        collider.points[4] = new Vector2(x, y);
    }
}
