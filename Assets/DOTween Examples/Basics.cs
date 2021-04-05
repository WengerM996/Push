using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Basics : MonoBehaviour
{
	public Transform redCube, greenCube, blueCube, purpleCube;
	[SerializeField] private float _speed;
	[SerializeField] private GameObject _character;

	private Rigidbody _rigidbodyStick;
	private Rigidbody _rigidbodyCharacter;

	private void Awake()
	{
		_rigidbodyStick = blueCube.GetComponent<Rigidbody>();
		_rigidbodyCharacter = _character.GetComponent<Rigidbody>();
	}

	IEnumerator Start()
	{
		// Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
		yield return new WaitForSeconds(1);

		// Let's move the red cube TO 0,4,0 in 2 seconds
		redCube.DOMove(new Vector3(0,4,0), 2);

		// Let's move the green cube FROM 0,4,0 in 2 seconds
		greenCube.DOMove(new Vector3(0,4,0), 2).From();
		
		// Let's move the blue cube BY 0,4,0 in 2 seconds
		//blueCube.DOMove();

		

		// Let's move the purple cube BY 6,0,0 in 2 seconds
		// and also change its color to yellow.
		// To change its color, we'll have to use its material as a target (instead than its transform).
		purpleCube.DOMove(new Vector3(6,0,0), 2).SetRelative();
		// Also, let's set the color tween to loop infinitely forward and backwards
		purpleCube.GetComponent<Renderer>().material.DOColor(Color.yellow, 2).SetLoops(-1, LoopType.Yoyo);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_rigidbodyStick.DOMove(new Vector3(0, 4, 0), 1).SetRelative().SetLoops(2, LoopType.Yoyo);
		}
		
		if (Input.GetKeyDown(KeyCode.A))
		{
			Move(Vector3.left);
		}
        
		if (Input.GetKeyDown(KeyCode.D))
		{
			Move(Vector3.right);
		}
	}
	
	private void Move(Vector3 direction)
	{
		print("moving " + direction);
		_rigidbodyCharacter.velocity = Vector3.zero;
		_rigidbodyCharacter.velocity = direction * _speed;
	}
}