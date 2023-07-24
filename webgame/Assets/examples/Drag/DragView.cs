using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace example
{
    public class DragView : UICounter
    {
        // Start is called before the first frame update
        public VisualTreeAsset asset;

        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                var item = root.Q<VisualElement>($"item_{i}");
                var label = item.Q<Label>("Label");
                label.text = $"hello_{i}";
                item.AddManipulator(new MyDrag());
                item.RegisterCallback<DropEvent>(OnDragevet2);
            }

            return;
            var prefab = root.Q<VisualElement>("draggable");
            for (int i = 0; i < 5; i++)
            {
                var item = prefab.visualTreeAssetSource.Instantiate();
                root.Add(item);
                var label = item.Q<Label>("draggable");
                label.text = $"{i}_hello";
                // item.RegisterCallback<MouseMoveEvent>(OnDown);
                // item.RegisterCallback<MouseUpEvent>(OnUp);
                // item.RegisterCallback<MouseUpEvent>(OnMove1);
                label.AddManipulator(new MyDrag());

                // item.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
                // item.RegisterCallback<MouseOverEvent>(OnMouseOver1);
                item.RegisterCallback<DropEvent>(OnDragevet);
            }

            prefab.style.display = DisplayStyle.None;
        }

        private void OnDragevet2(DropEvent evt)
        {
            var item = (VisualElement)evt.target;
            var label = item.Q<Label>("Label");
            //  Debug.Log($"{evt.target} dropped on {evt.droppable}");

            var item2 = (VisualElement)evt.droppable;
            if (item2 != null)
            {
                // var label2 = item2.Q<Label>("Label");
                // Debug.Log($"{label.text} dropped on {label2.text}");
                if (item != item2)
                    DragManipulator.ChangeParent(item, item2);
            }
            else
            {
            }
        }


        private void OnMouseOver1(MouseOverEvent evt)
        {
            var item = (VisualElement)evt.currentTarget;
            var label = item.Q<Label>("labelPrefab");
            Log("Over," + label.text);
        }

        private void OnMouseEnter(MouseEnterEvent evt)
        {
            var item = (VisualElement)evt.currentTarget;
            var label = item.Q<Label>("labelPrefab");
            Log("Enter," + label.text);
        }

        private DropEvent curEvt;

        private void OnDragevet(DropEvent evt)
        {
            curEvt = evt;
            var item = (VisualElement)evt.target;
            var label = item.Q<Label>("draggable");
            //  Debug.Log($"{evt.target} dropped on {evt.droppable}");

            var item2 = (VisualElement)evt.droppable;
            var label2 = item2.Q<Label>("draggable");
            Debug.Log($"{label.text} dropped on {label2.text}");
            DragManipulator.ChangeParent(item, item2);
        }

        private void OnMove1(MouseMoveEvent evt)
        {
        }

        private void OnUp(MouseUpEvent evt)
        {
        }

        private void OnDown(MouseDownEvent evt)
        {
        }

        public static IVisualElementScheduledItem ChangeParent(VisualElement target,
            VisualElement newParent)
        {
            var position_parent = target.ChangeCoordinatesTo(newParent, Vector2.zero);
            target.RemoveFromHierarchy();
            target.transform.position = Vector3.zero;
            newParent.Add(target);
            // ChangeCoordinatesTo will not be correct unless you wait a tick. #hardwon
            return target.schedule.Execute(() =>
            {
                var newPosition = position_parent - target.ChangeCoordinatesTo(newParent,
                    Vector2.zero);
                target.RemoveFromHierarchy();
                target.transform.position = newPosition;

                newParent.Add(target);
            });
        }
    }
}