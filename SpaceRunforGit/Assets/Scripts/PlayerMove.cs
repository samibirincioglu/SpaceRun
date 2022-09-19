using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
	public Rigidbody rb;
	public bool onRight = true;
	private float forwardSpeed = 8;
	private float jumpSpeed = 8;
	public float horizontal = 1;
	public bool isCollideWall;
	public bool isDead = false;
	bool DeathMethodCalled = false;
	public GameObject deathMenu;
	private Vector3 deadVelo;
	AudioSource jumpSound;
	private bool touched = false;
	private Touch touch;
	public GameObject SpawnManager;
	public bool gameStarted;

	private void Update()
	{
	    if (Input.touchCount > 0)
		{
			touch =  Input.GetTouch(0);
			if (touch.phase == UnityEngine.TouchPhase.Began)
			{
				Jump();
			}
		}
		if (isDead)
			transform.Rotate(0, 150 * Time.deltaTime, 0);


	}
	private void Start()
	{
		gameStarted = true; 
		jumpSound = GetComponent<AudioSource>();
		SpawnManager.SetActive(true);
		int[] randomStart = { 1, -1 };
		int randomPick = Random.Range(0, 2);
		horizontal = randomStart[randomPick];
		if (horizontal == -1)
			onRight = false;

		GameObject player = this.gameObject;
		player.GetComponent<Score>().gameObject.SetActive(true);
	}
	private void FixedUpdate()
	{
		if (isDead)
			return;
			//karakterin s�rekli bir �ekilde ileri hareket etmesi 
			Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;
			rb.MovePosition(rb.position + forwardMove);
			rb.velocity = new Vector3(jumpSpeed * horizontal, rb.velocity.y, rb.velocity.z);

	}
	private void LateUpdate()
	 {
		//karakter �ld�yse fonksiyonu �a��r
		if (isDead && !DeathMethodCalled)
		{
			Death();
			rb.velocity = deadVelo;
		}
		if(isDead)
			rb.velocity = deadVelo;

	}

		//MOBILE CONTROLS
		// *sa�dan sola z�plama*
		//klavye girdisini context ile al ve "context"e her bas�ld���nda
		//�artlar sa�lan�yorsa horizontal� de�i�tir gerisini fixedupdate k�sm� halleder
		public void Jump()
		{
			if (isDead || touched)
				return;

			if (onRight && isCollideWall )
			{
			   //onright ile bir dahaki z�plama i�in y�n belirle
				horizontal = -1;
				jumpSound.Play();
				onRight = !onRight;
			}
			else if ( !onRight && isCollideWall)
			{
				horizontal = 1;
				jumpSound.Play();
				onRight = !onRight;
			}

		}
	//  PC CONTROLS
    //public void Jump(InputAction.CallbackContext context)
    //{
    //    if (isDead || touched)
    //        return;

    //    if ( context.performed && onRight && isCollideWall)
    //    {
    //        onright ile bir dahaki z�plama i�in y�n belirle

    //        horizontal = -1;
    //        jumpSound.Play();
    //        onRight = !onRight;
    //    }
    //    else if ( context.performed && !onRight && isCollideWall)
    //    {
    //        horizontal = 1;
    //        jumpSound.Play();
    //        onRight = !onRight;
    //    }

    //}



    //karakterle kesi�en collider duvar ise karakteri duvarda g�steren iscollidewall i true yap
    private void OnTriggerStay(Collider collider)
		{
			if (collider.tag == "Wall")
			{
				
				isCollideWall = true;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.tag == "Wall")
			{
				isCollideWall = false;
			}
		}

		//�l�nce �a��r�lan fonk
		public void Death()
		{
			if (onRight)
				deadVelo = new Vector3(10, -10, rb.velocity.z);
			else
				deadVelo = new Vector3(-10, -10, rb.velocity.z);

			//update fonksiyonundan dolay� bu fonk s�rekli �a��r�lacak fakat bu if ko�ulundan dolay� sadece ilk seferde �al��acak
			if (DeathMethodCalled == true)
			{
				return;
			}
			//freefall
			rb.velocity =  deadVelo;
			Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;
			rb.MovePosition(rb.position + forwardMove * 5);
			DeathMethodCalled = true;
			//GameManager dan bir fonksiyon calistiracaginda assagidaki gibi almalisin
			//bunun icinde GameManager in icinde statik bir inst tanimlamasi olmasi gerek
			GameManager.inst.playerOnDead();
			

			Invoke("SpawnAds", 3);
			Invoke("callDeathMenu", 3);
		}

		void callDeathMenu()
		{
			deathMenu.SetActive(true);
		}

		void Restart()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			isDead = false;
		}

		//Score dan alaca�� zorluk de�eri ile h�zlar� y�kselt
		public void SetSpeed(float speedFactor)
		{
			forwardSpeed += speedFactor;
			jumpSpeed    += speedFactor + 1;
		}

	}
