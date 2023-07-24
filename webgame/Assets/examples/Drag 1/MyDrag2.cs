using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace example
{
    public delegate bool Call(PointerDownEvent evt);

    public class MyDrag2 : DragManipulator
    {
        public Call onDragBegin;

        public override void ResetPosition()
        {
            target.transform.position = Vector3.zero;
        }

        // protected override void DragBegin(PointerDownEvent ev)
        // {
        //     base.DragBegin(ev);
        //     if (onDragBegin(ev))
        //     {
        //     }
        // }
    }
}