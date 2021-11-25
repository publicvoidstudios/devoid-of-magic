using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowQuestPointer : MonoBehaviour
{
    [SerializeField]
    private Camera uICamera;
    private Vector3 targetPosition;
    [SerializeField]
    GameObject[] questTargets;
    Player player;
    private RectTransform pointerRectTransform;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = questTargets[player.level].transform.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }
    private void Update()
    {
        targetPosition = questTargets[player.level].transform.position;
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;

        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 250f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        if (isOffScreen || player.level == 1 || player.level == 3 || player.level == 5)
        {
            pointerRectTransform.gameObject.SetActive(true);
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = uICamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
        else
        {            
            pointerRectTransform.gameObject.SetActive(false);
        }
    }

}
