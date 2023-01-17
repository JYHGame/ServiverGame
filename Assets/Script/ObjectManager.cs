using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject expSPrefeb;
    public GameObject expMPrefeb;
    public GameObject expLPrefeb;

    public GameObject hpPortionPrefeb;
    public GameObject magnetPrefeb;

    public GameObject bulletSPrefeb;

    GameObject[] expS;
    GameObject[] expM;
    GameObject[] expL;

    GameObject[] hpPortion;
    GameObject[] magnet;

    GameObject[] bulletS;

    GameObject[] targetPool;

    private void Awake()
    {
        expS = new GameObject[100];
        expM = new GameObject[100];
        expL = new GameObject[100];

        hpPortion = new GameObject[10];
        magnet = new GameObject[10];

        bulletS = new GameObject[50];

        Generate();
    }
    void Generate()
    {
        for(int index = 0; index < expS.Length; index++)
        {
            expS[index] = Instantiate(expSPrefeb);
            expS[index].SetActive(false);
        }
        for (int index = 0; index < expM.Length; index++)
        {
            expM[index] = Instantiate(expMPrefeb);
            expM[index].SetActive(false);
        }
        for (int index = 0; index < expL.Length; index++)
        {
            expL[index] = Instantiate(expLPrefeb);
            expL[index].SetActive(false);
        }
        for (int index = 0; index < hpPortion.Length; index++)
        {
            hpPortion[index] = Instantiate(hpPortionPrefeb);
            hpPortion[index].SetActive(false);
        }
        for (int index = 0; index < magnet.Length; index++)
        {
            magnet[index] = Instantiate(magnetPrefeb);
            magnet[index].SetActive(false);
        }
        for (int index = 0; index < bulletS.Length; index++)
        {
            bulletS[index] = Instantiate(bulletSPrefeb);
            bulletS[index].SetActive(false);
        }

    }

    // Pool에 복제본 리스트 넣기. 클론 생성 대신 쓰는 함수
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "ExpS":
                targetPool = expS;
                break;
            case "ExpM":
                targetPool = expM;
                break;
            case "ExpL":
                targetPool = expL;
                break;
            case "HpPortion":
                targetPool = hpPortion;
                break;
            case "Magnet":
                targetPool = magnet;
                break;
            case "BulletS":
                targetPool = bulletS;
                break;
        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "ExpS":
                targetPool = expS;
                break;
            case "ExpM":
                targetPool = expM;
                break;
            case "ExpL":
                targetPool = expL;
                break;
            case "HpPortion":
                targetPool = hpPortion;
                break;
            case "Magnet":
                targetPool = magnet;
                break;
            case "BulletS":
                targetPool = bulletS;
                break;
        }
        return targetPool;
    }
}
