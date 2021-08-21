using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
    [SerializeField] GameObject blade, cube;
    [SerializeField] Vector3 rotation;
    Vector2 start, end;


    public void SetStartAndEndPoints(Vector2 start, Vector2 end)
    {
        this.start = start;
        this.end = end;
    }

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void Update()
    {


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(end - start / 2);
        if (Physics.Raycast(ray, out hit))
        {
            blade.transform.position = Vector3.MoveTowards(blade.transform.position, hit.point, 1f);
        }

        //blade.transform.position += Vector3.forward;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {

        Vector2 newBladePosition = end - start;
        blade.transform.position = newBladePosition / 2 + start;
        blade.transform.position += new Vector3(0, 0, -1);

        blade.transform.rotation = Quaternion.FromToRotation(Vector3.up, newBladePosition);
        blade.transform.Rotate(rotation);

        Vector3 scale = blade.transform.localScale;
        scale.y = newBladePosition.magnitude;
        transform.localScale = scale;


    }
}