using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Shape
{
    /* INHERITANCE
     * Star is a shape that modifies its movement direction.
     */

    public bool right;

    private void Start()
    {
        if(!right) {
            rotationSpeed *= -1;
        }
    }

    /* POLYMORPHISM
     * Override GetMovementDirection() method to change default movement direction of the object.
     */
    protected override Vector3 GetMovementDirection() {
        Vector3 direction = Vector3.down + (right ? Vector3.right : Vector3.left);

        return direction.normalized;
    }
}
