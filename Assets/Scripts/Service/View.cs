using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// displays the points of each unit
/// </summary>
public class View : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreView;
    [SerializeField] private FloatingJoystick _floatingJoystick;
    [SerializeField] private Transform _camera;
    [Header("Score")] 
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _offset;
    
    public static List<ScoreView> ScoreViews { get; set; }
    public static View Instance { get; private set; }

    public static event UnityAction<ScoreView> RequestAddView;
    
    public FloatingJoystick FloatingJoystick { get => _floatingJoystick; }

    public TMP_Text ScoreView => _scoreView;


    private void OnEnable()
    {
        RequestAddView += OnRequestAddView;
    }

    private void OnDisable()
    {
        RequestAddView -= OnRequestAddView;
    }

    private void Awake()
    {
        Instance = this;
        ScoreViews = new List<ScoreView>();
        _scoreView.text = "Best: " + PlayerPrefs.GetInt("Best", 0);
    }

    public static ScoreView AddScoreView(ScoreView viewTemplate)
    {
        var position = Instance._startPosition;
        
        if (ScoreViews.Count > 0)
        { 
            position = ScoreViews[ScoreViews.Count - 1].transform.position;
            position += Instance._offset;
        }
        
        var view = Instantiate(viewTemplate, position, Quaternion.identity, Instance._camera);
        ScoreViews.Add(view);
        
        //RequestAddView?.Invoke(view);
        
        return view;
    }

    private void OnRequestAddView(ScoreView view)
    {
        view.transform.SetParent(_camera);
        var position = _startPosition;
        
        if (ScoreViews.Count > 0)
        { 
            position = ScoreViews[ScoreViews.Count - 1].transform.position;
            //position = ScoreViews[ScoreViews.Count - 1].GetComponent<RectTransform>().position;
            position += _offset;
        }
        
        view.transform.position = position;
        //view.GetComponent<RectTransform>().position = position;
        ScoreViews.Add(view);
    }
}
