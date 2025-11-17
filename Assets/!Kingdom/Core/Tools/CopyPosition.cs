using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] Transform source;
    [SerializeField] Transform target;

    [SerializeField] bool isXCopied = false;
    [SerializeField] bool isYCopied = false;
    [SerializeField] bool isZCopied = false;
    // Update is called once per frame
    void Update()
    {
        if (isXCopied) target.position = new Vector3(source.position.x, target.position.y, target.position.z);
        if (isYCopied) target.position = new Vector3(target.position.x, source.position.y, target.position.z);
        if (isZCopied) target.position = new Vector3(target.position.x, target.position.y, source.position.z);
    }
}
