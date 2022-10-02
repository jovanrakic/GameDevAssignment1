using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class ThirdPersonShootingController : MonoBehaviour{

	[SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
	[SerializeField] private Animator animator;
	[SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
	[SerializeField] private Transform debugTransform;
	[SerializeField] private Transform bulletProjectile;
	[SerializeField] private Transform bulletSpawnPoint;


	AudioSource laser_01;
	private bool aiming = false;
	

	private StarterAssetsInputs starterAssetsInputs;
	private ThirdPersonController thirdPersonController;

	private void Awake(){
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		thirdPersonController = GetComponent<ThirdPersonController>();
		laser_01 = GetComponent<AudioSource>();
	}

	private void Update() {
		Vector3 mouseWorldPosition = Vector3.zero;
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
			debugTransform.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
		}
		if (starterAssetsInputs.aim){
			aiming = true;
			thirdPersonController.RotateOnAim(false);
			aimVirtualCamera.gameObject.SetActive(true);
			animator.SetBool("Aiming", true);
			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;


			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); // Turns player to target
			
		}else{
			aiming = false;
			aimVirtualCamera.gameObject.SetActive(false);
			animator.SetBool("Aiming", false);
			thirdPersonController.RotateOnAim(true);
		}

		if (starterAssetsInputs.shoot && aiming == true){
			laser_01.Play();
			Vector3 aimDir =(mouseWorldPosition - bulletSpawnPoint.position).normalized;
			Instantiate(bulletProjectile, bulletSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
			starterAssetsInputs.shoot = false;
		}

		
	}
}