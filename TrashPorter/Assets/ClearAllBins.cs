using UnityEngine;
using System.Collections;

public class ClearAllBins : MonoBehaviour
{
    public BinScoreManager[] bins;
    public Transform handle;
    public float binMoveDistance = 2f;
    public float binMoveDuration = 0.5f;

    private bool isActivated = false;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = handle.rotation;
    }

    private void OnMouseDown()
    {
        if (!isActivated)
        {
            isActivated = true;
            StartCoroutine(RaiseAndLowerLever());
            StartCoroutine(MoveBinsAndClear());
        }
    }

    private IEnumerator RaiseAndLowerLever()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Quaternion startRotation = handle.rotation;
        Quaternion targetRotation = Quaternion.Euler(-45, 0, 0) * initialRotation;

        while (elapsed < duration)
        {
            handle.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        handle.rotation = targetRotation;
        yield return new WaitForSeconds(0.5f);

        elapsed = 0f;
        while (elapsed < duration)
        {
            handle.rotation = Quaternion.Slerp(targetRotation, initialRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        handle.rotation = initialRotation;
        isActivated = false;
    }

    private IEnumerator MoveBinsAndClear()
    {
        foreach (BinScoreManager bin in bins)
        {
            StartCoroutine(bin.MoveBackwardAndClear(binMoveDistance, binMoveDuration));
        }

        yield return new WaitForSeconds(binMoveDuration + 1f);
        Debug.Log("Toutes les poubelles ont été vidées et replacées !");
    }
}
