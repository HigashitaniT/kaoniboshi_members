using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingScript : MonoBehaviour {

    [SerializeField]
    private BodySourceView _bodyScript;

    [SerializeField]
    private GameController _gameCtrl;

    [SerializeField]
    private GameObject missileObj;

    [SerializeField]
    private int shakePoint;

    [SerializeField]
    private float delay,timeRate,speed;

    [SerializeField]
    private Transform missilePar;

    private Vector3 lHandPos, rHandPos, headPos, leftNormalized, rightNormalized;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        timeRate += Time.deltaTime;

        //if (timeRate >= delay)
        //{
        //    timeRate = 0;
        //    Shot();
        //}
        if (_bodyScript._ChargePoint >= shakePoint)
        {
            Shot();
            _bodyScript._ChargePoint = 0;
        }

        if (_bodyScript.IsTraked)
        {
            //GameObject body = _bodyScript.FirstBody().transform.FindChild("")
            
            lHandPos = _bodyScript.FirstBody().transform.FindChild("HandLeft").transform.position;
            rHandPos = _bodyScript.FirstBody().transform.FindChild("HandRight").transform.position;
            headPos = _bodyScript.FirstBody().transform.FindChild("Head").transform.position;
//            lHandPos = _bodyScript.LeftHandPosition();
//            rHandPos = _bodyScript.RightHandPosition();
//            headPos = this.transform.position;

            leftNormalized = headPos - lHandPos;
            rightNormalized = headPos - rHandPos;

            //energyGage.value = (float)_bodyScript._ChargePoint;
            //if(_bodyScript._ChargePoint >= shotPoint)
            //{

            //}
            //_bodyScript._ChargePoint

        }
	}

    private void Shot()
    {
        //Debug.logger.Log("<color=#0000ffff>title</color>",leftNormalized);

        //Vector3 rotL = new Vector3(leftNormalized.x, leftNormalized.y, 0);
        //Vector3 rotR = new Vector3(rightNormalized.x, rightNormalized.y, 0);

        //GameObject missileClornL = Instantiate(missileObj, headPos, Quaternion.FromToRotation(Vector3.up,rotL));
        //GameObject missileClornR = Instantiate(missileObj, headPos, Quaternion.FromToRotation(Vector3.up, rotR));

        StartCoroutine("InstanceMissile");


        //missileClornL.GetComponent<Rigidbody>()
        //    .AddForce(-missileClornL.transform.up * speed);
        //missileClornR.GetComponent<Rigidbody>()
        //    .AddForce(-missileClornR.transform.up * speed);

    }

    public IEnumerator InstanceMissile()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 vec3 = new Vector3((Random.insideUnitSphere.x*360), 90, 0);
            Quaternion randomQuat = new Quaternion(0, 90, 0, 0);

            randomQuat = Quaternion.Euler(vec3);

            GameObject missileClorn = Instantiate(missileObj, headPos, randomQuat,missilePar);
            SoundManeger.Instance.isPlayMissileSe = true;

            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

}
