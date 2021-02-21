using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public abstract class Weapon : MonoBehaviourPun
{

    private GameObject ShootPoint;
    private int WeaponDamage;
    private string WeaponName;

    public Weapon(string WeaponName, int WeaponDamage)
    {
        this.WeaponName = WeaponName;
        this.WeaponDamage = WeaponDamage;
    }

    public void Shoot()
    {
        //if(photonView.)
        photonView.RPC("RpcShoot", RpcTarget.All);
    }

    [PunRPC]
    void RpcShoot()
    {
        GameObject bullet = Instantiate((GameObject)Resources.Load("45ACP Bullet_Head"), ShootPoint.transform.position, ShootPoint.transform.rotation);
        Rigidbody br = bullet.GetComponent<Rigidbody>();
        br.AddRelativeForce(Vector3.forward * 1000 * Time.deltaTime, ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {

        //foreach (Transform t in gameObject.transform)
        //{
        //    if (t.tag == "Bullet")
        //    {
        //        ShootPoint = t.gameObject;
        //    }
        //}
        WeaponStart();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponUpdate();
    }

    public abstract void WeaponStart();
    public abstract void WeaponUpdate();


}
