using DialogueEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationStarter : MonoBehaviour
{
    [Header("Conversation")]
    [SerializeField] private NPCConversation myConversation;

    [Header("Mobile / UI")]
    [SerializeField] private Button mobileButton;         // assign your Talk button
    [SerializeField] private GameObject mobileButtonRoot; // parent panel for the button

    [Header("Player Detection")]
    [SerializeField] private string playerTag = "Player";

    private bool playerInside = false;

    private void Awake()
    {
        if (mobileButton != null)
        {
            mobileButton.onClick.AddListener(OnMobileButtonPressed);
            if (mobileButtonRoot != null) mobileButtonRoot.SetActive(false); // hide at start
        }
    }

    private void OnDestroy()
    {
        if (mobileButton != null)
            mobileButton.onClick.RemoveListener(OnMobileButtonPressed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = true;
            Debug.Log("[ConversationStarter] Player entered trigger zone.");

            // Show mobile button
            if (mobileButtonRoot != null)
            {
                mobileButtonRoot.SetActive(true);
                StartCoroutine(PopButtonAnimation(mobileButtonRoot));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInside = false;
            Debug.Log("[ConversationStarter] Player exited trigger zone.");

            if (mobileButtonRoot != null)
                mobileButtonRoot.SetActive(false);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            TryStartConversation();
        }
#endif
    }

    private void OnMobileButtonPressed()
    {
        Debug.Log("[ConversationStarter] Mobile button pressed.");
        if (playerInside)
            TryStartConversation();
        else
            Debug.Log("[ConversationStarter] Button pressed but player is NOT inside trigger.");
    }

    private void TryStartConversation()
    {
        if (myConversation == null)
        {
            Debug.LogWarning("[ConversationStarter] myConversation is not assigned in the Inspector.");
            return;
        }

        ConversationManager.Instance.StartConversation(myConversation);
        Debug.Log("[ConversationStarter] Conversation started.");
    }

    // Simple pop animation coroutine (scale up and back)
    private IEnumerator PopButtonAnimation(GameObject button)
    {
        Vector3 originalScale = button.transform.localScale;
        Vector3 targetScale = originalScale * 1.1f;
        float duration = 0.2f;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(originalScale, targetScale, t / duration);
            yield return null;
        }

        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(targetScale, originalScale, t / duration);
            yield return null;
        }
    }
}
