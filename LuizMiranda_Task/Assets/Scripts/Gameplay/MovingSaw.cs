using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    #region VARIABLES
    [Header ("Movement Settings")]
    [SerializeField] Vector2 direction = Vector2.right;
    [SerializeField] float distance = 3f;
    [SerializeField] float speed = 3f;

    [Header ("Original Position")]
    Vector3 startPosition;
    #endregion

    #region EVENTS
    void Start()
    {
        startPosition = transform.position;
        direction = direction.normalized;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);

        transform.position = startPosition + (Vector3)(direction * movement);
    }
    #endregion

    #region METHODS
    #endregion
}
