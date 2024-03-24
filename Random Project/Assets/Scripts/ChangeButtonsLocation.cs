using System.Collections;
using UnityEngine;

public class ChangeButtonsLocation : MonoBehaviour
{
    [SerializeField] private RectTransform jumpButton;
    [SerializeField] private RectTransform shootButton;
    [SerializeField] private RectTransform dashButton;

    private Vector2 initJumpButtonAnchoredPosition;
    private Vector2 initShootButtonAnchoredPosition;
    private Vector2 initDashButtonAnchoredPosition;

    private void Awake()
    {
        initJumpButtonAnchoredPosition = jumpButton.anchoredPosition;
        initShootButtonAnchoredPosition = shootButton.anchoredPosition;
        initDashButtonAnchoredPosition = dashButton.anchoredPosition;
    }

    private void OnEnable()
    {
        VoltageObstacle.OnPlayerHitVoltageBall += TemporarilyChangeButtonsLocations;
    }

    private void OnDisable()
    {
        VoltageObstacle.OnPlayerHitVoltageBall -= TemporarilyChangeButtonsLocations;
    }

    private void ChangeLocationsOfButtons()
    {
        Vector2 jumpButtonAnchoredPos = initJumpButtonAnchoredPosition;
        Vector2 shootButtonAnchoredPos = initShootButtonAnchoredPosition;
        Vector2 dashButtonAnchoredPos = initDashButtonAnchoredPosition;

        jumpButton.anchoredPosition = shootButtonAnchoredPos;
        shootButton.anchoredPosition = dashButtonAnchoredPos;
        dashButton.anchoredPosition = jumpButtonAnchoredPos;
    }

    private void ResetButtonLocations()
    {
        jumpButton.anchoredPosition = initJumpButtonAnchoredPosition;
        shootButton.anchoredPosition = initShootButtonAnchoredPosition;
        dashButton.anchoredPosition = initDashButtonAnchoredPosition;
    }

    public void TemporarilyChangeButtonsLocations(float duration)
    {
        StartCoroutine(TemporarilyChangeButtonsLocationsCoroutine(duration));
    }

    private IEnumerator TemporarilyChangeButtonsLocationsCoroutine(float duration)
    {
        ChangeLocationsOfButtons();
        yield return new WaitForSeconds(duration);
        ResetButtonLocations();
    }
}
