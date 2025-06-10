using UnityEngine;

public class Cube : MonoBehaviour
{
    private Vector3 rotationAxis;
    private float rotationSpeed;

    private void Start() {
        Randomize();
    }
    
    public void Randomize() {
        // Reinitialize the rotation axis and speed
        rotationAxis = Random.onUnitSphere;
        rotationSpeed = Random.Range(20f, 100f);
    }

    private void Update() {
        // Rotate the cube around the random axis each frame
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}