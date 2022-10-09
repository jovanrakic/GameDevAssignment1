using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class ThirdPersonShootingController : MonoBehaviour{

	[SerializeField] private CinemachineVirtualCamera aimVirtualCamera; //Aim camera object
	[SerializeField] private Animator animator; //Player animations
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();// Target's collision types(Everything)
	[SerializeField] private Transform debugTransform;// Hidden target position
	[SerializeField] private Transform bulletProjectile;// The players bullet
	[SerializeField] private Transform bulletSpawnPoint;// The spawnpoint of the bullet


	AudioSource laser_01;
	private bool aiming = false;// Aiming is set to false by default
	

	private StarterAssetsInputs starterAssetsInputs;// Detect player Input
	private ThirdPersonController thirdPersonController;// Player movements
	// Initialising all components in awake
	private void Awake(){
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		thirdPersonController = GetComponent<ThirdPersonController>();
		laser_01 = GetComponent<AudioSource>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update() {
		if (Input.GetKeyDown("escape")){// Exit to main menu
			SceneManager.LoadScene("TitlePage");
		}
		// Calculating aiming position based on mouse cursor
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);// Converting mousePosition from 3D to 2D space
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);// Sending invisible ray for hit collisions
		if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {//If ray hits any game object
			debugTransform.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
		}
		// If player aim is true, start aim animation
		if (starterAssetsInputs.aim){
			aiming = true;
			thirdPersonController.RotateOnAim(false);// Disable player rotations in Aim
			aimVirtualCamera.gameObject.SetActive(true);// Activate Aim Camera
			animator.SetBool("Aiming", true);// Start aim animations
			Vector3 worldAimTarget = mouseWorldPosition;// Calculates the mousePosition to shoot
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;// Calculate the direction of the bullet based on aim


			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); // Turns player to mousePosition
			
		}else{// Enable opposites
			aiming = false;
			aimVirtualCamera.gameObject.SetActive(false);
			animator.SetBool("Aiming", false);
			thirdPersonController.RotateOnAim(true);
		}
		// If aim is true and shoot is triggered, spawn a bullet
		if (starterAssetsInputs.shoot && aiming == true){
			laser_01.Play();
			Vector3 aimDir =(mouseWorldPosition - bulletSpawnPoint.position).normalized;
			Instantiate(bulletProjectile, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
			starterAssetsInputs.shoot = false;
		}

		
	}
}