using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Shape
{
    /* INHERITANCE
     * Triangle is a shape that increases its speed over time.
     */
    public float acceleration = 2f;


    /* POLYMORPHISM
     *  - Override Update() to increase movementSpeed.
     */

    protected override void Update()
    {
        Accelerate();
        base.Update();
    }

    private void Accelerate() {
        movementSpeed += acceleration * Time.deltaTime;
    }
}
