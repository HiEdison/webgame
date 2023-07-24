using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace example
{
    public class DragTest : UICounter
    {
        private void Start()
        {
            DragAndDropManipulator manipulator =
                new(root.Q<VisualElement>("object"));
        }
    }
}