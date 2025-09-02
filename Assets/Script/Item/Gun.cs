using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range;
    public float accuracy;
    public float fireRate; // 연사속도
    public float reloadTime;

    public int damage;
    public int reloadBulletCount; // 총의 재장전 개수
    public int currentBulletCount; // 현재 탄알집에 남아있는 총알의 개수
    public int maxBulletCount; // 최대 소유 개수
    public int carryBulletCount; // 현재 소유하고 있는 총알의 개수

    public float retroActionForce; // 반동세기
    public float retroActionFineSightForce; // 정조준시의 반동 세기

    public Vector3 findSightOriginPos; // 총 위치 변경
    public Animator animator;
    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;

}
