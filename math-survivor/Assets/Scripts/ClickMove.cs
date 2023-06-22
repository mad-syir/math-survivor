using UnityEngine;

public class ClickMove : MonoBehaviour
{
    public Transform target; // Reference to the target object
    public float speed = 5f; // Speed at which the target moves towards the object

    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            // Calculate the direction from the target position to the current object position
            Vector2 direction = (transform.position - target.position).normalized;

            // Move the target object towards the current object
            target.Translate(direction * speed * Time.deltaTime);

            // If the target object has reached the current object, stop moving
            if (Vector2.Distance(target.position, transform.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    private void OnMouseDown()
    {
        isMoving = true;
    }
}
