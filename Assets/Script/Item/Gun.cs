using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range;
    public float accuracy;
    public float fireRate; // ����ӵ�
    public float reloadTime;

    public int damage;
    public int reloadBulletCount; // ���� ������ ����
    public int currentBulletCount; // ���� ź������ �����ִ� �Ѿ��� ����
    public int maxBulletCount; // �ִ� ���� ����
    public int carryBulletCount; // ���� �����ϰ� �ִ� �Ѿ��� ����

    public float retroActionForce; // �ݵ�����
    public float retroActionFineSightForce; // �����ؽ��� �ݵ� ����

    public Vector3 findSightOriginPos; // �� ��ġ ����
    public Animator animator;
    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;

}
