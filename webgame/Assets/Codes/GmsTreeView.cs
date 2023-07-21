using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GmsTreeView : UICounter
{
    private ListView _listView;
    public VisualTreeAsset prefab;

    void Start()
    {
        _listView = root.Q<ListView>("ListView");
        _listView.selectionType = SelectionType.Multiple;

        _listView.onItemsChosen += OnItemsChosen;
        _listView.onSelectionChange += OnSelectionChange;
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

        RefreshEvent();
    }

    private GameObject[] _objs;

    private void RefreshEvent()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        _objs = currentScene.GetRootGameObjects();
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