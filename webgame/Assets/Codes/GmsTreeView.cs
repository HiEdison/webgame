using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

//https://docs.unity.cn/Manual/UIE-uxml-element-TextField.html
public class GmsTreeView : UICounter
{
    private ListView _listView;
    private TextField _search;
    public VisualTreeAsset prefab;
    private Label fps;
    private GameObject[] _objs;

    protected override void Awake()
    {
        base.Awake();
        _listView = root.Q<ListView>("ListView");
        _search = root.Q<TextField>("search");
        fps = root.Q<Label>("fps");

        _listView.selectionType = SelectionType.Multiple;

        _listView.onItemsChosen += OnItemsChosen;
        _listView.onSelectionChange += OnSelectionChange;

        _search.value = "";
#if UNITY_EDITOR
        _search.RegisterCallback<KeyDownEvent>(OnSearchCallback, TrickleDown.TrickleDown);
#endif
        _search.RegisterValueChangedCallback(OnSearchChangeed);
    }

    private void OnSearchChangeed(ChangeEvent<string> evt)
    {
        if (string.IsNullOrEmpty(evt.newValue))
        {
            SearchGo();
        }
        else
        {
            SearchGo();
        }
    }

    private void Update()
    {
        fps.text = string.Format("Fps:{0} ", (int)Frame._Fps);
    }

    private void OnSearchCallback(KeyDownEvent evt)
    {
        if (evt.keyCode == KeyCode.Return)
        {
            // Submit logic
            // evt.StopPropagation();
            // evt.PreventDefault();
            SearchGo();
        }

        if (evt.modifiers == EventModifiers.Shift && (evt.keyCode == KeyCode.Tab || _search.text == @"/t"))
        {
            // Focus logic
            evt.StopPropagation();
            evt.PreventDefault();
        }
    }

    void SearchGo()
    {
        if (!string.IsNullOrEmpty(_search.text))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            var objs = currentScene.GetRootGameObjects();
            List<GameObject> newlist = new List<GameObject>();
            var letstr = _search.text.ToLower();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].name.ToLower().StartsWith(letstr))
                {
                    newlist.Add(objs[i]);
                }
            }
            _objs = newlist.ToArray();
        }
        else
        {
            Scene currentScene = SceneManager.GetActiveScene();
            _objs = currentScene.GetRootGameObjects();
        }
        RefreshEvent();
    }


    private void OnSelectionChange(IEnumerable<object> obj)
    {
        foreach (var item in obj)
        {
            Debug.Log(item);
        }
    }

    private void OnItemsChosen(IEnumerable<object> obj)
    {
        foreach (var item in obj)
        {
            Debug.Log(item);
        }
    }

    public void AllReady(GameObject[] obj)
    {
        GameObject[] ls = obj;
        List<string> items = new List<string>();
        for (int i = 0; i < ls.Length; i++)
        {
            items.Add(ls[i].name);
        }

        //RefreshEvent();
        SearchGo();
    }


    private void RefreshEvent()
    {
        // Scene currentScene = SceneManager.GetActiveScene();
        // _objs = currentScene.GetRootGameObjects();
        _listView.itemsSource = _objs;
        _listView.makeItem = MakeListItem;
        _listView.bindItem = BindListItem;
    }

    private VisualElement MakeListItem()
    {
        return prefab.Instantiate().Q<Label>("Label");
        // var label = new Label();
        // label.style.unityTextAlign = TextAnchor.MiddleCenter;
        // label.style.marginLeft = 5;
        // return label;
    }

    private void BindListItem(VisualElement arg1, int index)
    {
        Label label = arg1 as Label;
        var go = _objs[index];
        label.text = go.name;
    }

    //---------------------------------------

    // Update is called once per frame
    void AllReady2(GameObject[] obj)
    {
        GameObject[] ls = obj;
        List<string> items = new List<string>();
        for (int i = 0; i < ls.Length; i++)
        {
            items.Add(ls[i].name);
        }

        Func<VisualElement> makeItem = () => new Label();
        Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];
        const int itemHeight = 50;
        var listView = new ListView(items, itemHeight, makeItem, bindItem);
        listView.selectionType = SelectionType.Multiple;

        listView.onItemsChosen += objects => Debug.Log(objects);
        listView.onSelectionChange += objects => Debug.Log(objects);

        listView.style.flexGrow = 1.0f;

        root.Add(listView);
    }
}