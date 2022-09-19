using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform target;
    private Vector3 offset;
    PlayerMove playermove;
    public Transform camStartTarget;
    [SerializeField]
    private float[] distances;

    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playermove = g.GetComponent<PlayerMove>();
        target = g.transform;
        offset = transform.position - target.position;


        CameraMoveAtGameStart();
    }
    public void FindPlayer()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        playermove = g.GetComponent<PlayerMove>();
        target = g.transform;
        offset = transform.position - target.position;
    }
    public void CameraMoveAtGameStart()
    {

        Vector3 gameCameraAngle = new Vector3(camStartTarget.position.x, camStartTarget.position.y, camStartTarget.position.z);
        transform.rotation = camStartTarget.rotation;
        transform.position = Vector3.Lerp(transform.position,gameCameraAngle, 5f);
        offset = transform.position - target.position;
        
    }

    public void LateUpdate()
    {
        if (playermove.isDead == false)//Karakter hayattaysa normal bir þekilde takip et
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, 1f);
        }
        else//karakter öldüyse takip eden kameraya geç
        {
            Vector3 cameraPos = new Vector3(transform.position.x , offset.y + target.position.y , offset.z + target.position.z);
            transform.position = Vector3.Lerp(transform.position, cameraPos , 5f);
                transform.LookAt(target);
        }
        
    }

}
