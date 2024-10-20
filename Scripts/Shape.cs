using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    /* INHERITANCE */

    protected   int     m_value;
    [SerializeField]
    protected   float   movementSpeed;
    [SerializeField]
    protected   float   rotationSpeed;

    /* ENCAPSULATION */
    public int Value
    {
        get { return m_value; }
        set { m_value = Math.Max(value, 1); }
    }

    /* ABSTRACTION */
    protected virtual void Update()
    {
        Move();
        Rotate();
    }

    public virtual void Pop() {
        if (GameManager.Instance.GameEnded) return;

        GameManager.Instance.IncreaseScore(Value);

        Destroy(gameObject);
    }

    protected void OnMouseDown() {
        Pop();
    }

     /* POLYMORPHISM */
    protected void Move(Vector3 direction, float speed) {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    protected void Move() { 
        Move(GetMovementDirection(), movementSpeed);
    }

    protected virtual void Rotate() {
        if (rotationSpeed == 0) return;

        transform.Rotate(Vector3.forward * Mathf.PI * Time.deltaTime);
        Quaternion rot = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up);

        transform.rotation *= rot;
    }

    protected virtual Vector3 GetMovementDirection() {
        return Vector3.down;
    }
}
