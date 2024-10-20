using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    /* INHERITANCE
     * Circle is a shape that explodes in two when pops for the first time.
     */
    public const float EXPLOSION_OFFSET = 2f;

    private bool    exploded;
    private Vector3 offset;

    /* POLYMORPHISM
     *  - Override Pop() method to make it split in two first time it is touched.
     *  - Override Update() to apply additional movement when it is split in two.
     */
    public override void Pop() {
        if (GameManager.Instance.GameEnded) return;

        if(exploded) { 
            base.Pop();
        }
        else {
            SplitInTwo();
        }
    }

    protected override void Update()
    {
        UpdateOffset();
        base.Update();
    }

    /* ABSTRACTION
     *  SplitInTwo() is a higher-level method that hides all logic behind this operation,
     *  which include creating another circle, update their offsets and modify its shape
     */
    private void SplitInTwo() { 
        Circle secondCircle = Instantiate(gameObject, transform.position, transform.rotation).GetComponent<Circle>();

        this.offset         = Vector3.right * EXPLOSION_OFFSET;
        secondCircle.offset = Vector3.left  * EXPLOSION_OFFSET;

        this.TransformToExplodedCircle();
        secondCircle.TransformToExplodedCircle();

        secondCircle.Value = this.Value;
    }

    private void TransformToExplodedCircle() { 
        exploded = true;

        transform.localScale /= 1.5f;
    }

    private void UpdateOffset() {
        float distance = offset.magnitude;
        if (distance == 0f) return;

        // Move it and reduce offset
        Move(offset.normalized, movementSpeed * distance);
        offset = offset.normalized * Mathf.Max(distance - movementSpeed * distance * Time.deltaTime, 0);
    }
}
