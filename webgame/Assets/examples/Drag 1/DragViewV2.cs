using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace example
{
    public class DragViewV2 : UICounter
    {
        public List<VisualElement> cls = new List<VisualElement>();
        public Dictionary<VisualElement, VisualElement> linkDic = new Dictionary<VisualElement, VisualElement>();

        void Start()
        {
            for (int i = 0; i < 2; i++)
            {
                var item = root.Q<VisualElement>($"item_{i}");
                var label = item.Q<Label>("Label");
                label.text = $"hello_{i}";
                DragAndDropManipulator manipulator =
                    new(item);
                
                // var drag = new MyDrag2();
                // item.AddManipulator(drag);
                // item.RegisterCallback<DropEvent>(OnDragevet2);
                // drag.onDragBegin = OnTestDrag;
                cls.Add(item);
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
                item.RegisterCallback<DropEvent>(OnDragevet2);
            }

            prefab.style.display = DisplayStyle.None;
        }

        private bool OnTestDrag(PointerDownEvent obj)
        {
            var item = (VisualElement)obj.target;
            if (item.ClassListContains("droppable"))
            {
                root.Add(item);
                return true;
            }

            return false;
        }

        private void OnDragevet2(DropEvent evt)
        {
            var item = (VisualElement)evt.target;
            var label = item.Q<Label>("Label");
            // Debug.Log($"{evt.target} dropped on {evt.droppable}");

            var item2 = (VisualElement)evt.droppable;
            Debug.Log($"{item.name} 期望放在  {item2.parent.name} 之上，做儿子");
            if (item2 != null)
            {
                // var label2 = item2.Q<Label>("Label");
                // Debug.Log($"{label.text} dropped on {label2.text}");
                if (item != item2 && item2.parent != item)
                    ChangeParent(item, item2);
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


        private void OnMove1(MouseMoveEvent evt)
        {
        }

        private void OnUp(MouseUpEvent evt)
        {
        }

        private void OnDown(MouseDownEvent evt)
        {
        }

        public void ChangeParent(VisualElement target,
            VisualElement newParent)
        {
     
            return;
            if (IsChildren(target, newParent))
            {
                root.Add(newParent.parent);
                return;
            }
            else
            {
            }

            if (!linkDic.TryGetValue(target, out var a))
            {
                newParent.Add(target);
                linkDic[target] = newParent;
            }
            else
            {
                newParent.Add(target);
                linkDic[target] = newParent;
            }

            //
            // else
            // {
            //     if (IsChildren(target, newParent))
            //     {
            //         Debug.Log($"{target} is children==>{newParent}");
            //     }
            //     else if (IsParent(target, newParent))
            //     {
            //         Debug.Log($"{target} is parent==>{newParent}");
            //     }
            //     else
            //     {
            //         newParent.Add(target);
            //         linkDic[target] = newParent;
            //     }
            // }
        }

        bool IsChildren(VisualElement parent,
            VisualElement children)
        {
            var temp = children.parent;
            while (temp != null)
            {
                if (parent == temp)
                {
                    return true;
                }
                else
                {
                    temp = temp.parent;
                }
            }

            return false;
        }

        bool IsParent(VisualElement target,
            VisualElement newParent)
        {
            var ls = target.Children();
            if (ls.Count() > 0)
            {
                foreach (var item in ls)
                {
                    if (newParent == item)
                    {
                        return true;
                    }

                    if (IsParent(item, newParent))
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}