using UnityEngine;
using UnityEngine.UI;

public class RestoreObject : MonoBehaviour
{
    [SerializeField] Toggle slice;
    [SerializeField] RayFire.RayfireRigid sliceCube;
    [SerializeField] Toggle boxes;
    [SerializeField] RayFire.RayfireShatter boxesCube;
    public void Restore()
    {
        if (slice.isOn)
        {
            sliceCube.ResetRigid();
        }

        if (boxes.isOn)
        {
            boxesCube.DeleteFragmentsLast();
            boxesCube.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
