using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    //视角差游戏对象的初始位置
    Vector2 startingPosition;
    //初始对象的Z值
    float startingZ;

    //相机从视差物体的起始位置移动的距离
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // 如果物体在目标前方，使用近夹面。如果在目标后面，使用farClipPlane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    //物体离玩家越远，ParallaxEffect物体的移动速度就越快。拖动它的Z值靠近目标，使它移动得更慢。
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //当目标移动时，将视角差对象位移动相同的距离乘于一个数
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        //x,y根据目标移动数据变动，z轴不变
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
