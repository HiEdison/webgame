using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace example
{
    public class MyDrag : DragManipulator
    {
        public override void ResetPosition()
        {
            target.transform.position = Vector3.zero;
        }
    }
}