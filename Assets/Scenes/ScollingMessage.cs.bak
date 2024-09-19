using UnityEngine;
using TMPro;

public class ScrollingMessage : MonoBehaviour
{
    public float scrollSpeed = 150f; // Speed of the scrolling
    public float bounceHeight = 100f; // Maximum height of the bounce
    public float bounceDuration = 2f;  // Duration for one complete bounce cycle
    public float bottomOffset = -50f; // Y position where letters bounce off

    private TextMeshProUGUI textMeshPro; // TextMeshPro component

    public AudioClip backgroundMusic; // Background music clip
    private AudioSource audioSource; // AudioSource component

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = "Welcome to the 3D Experience! Enjoy the nostalgia!";
        ResetPosition(); // Start just outside the screen

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; // Loop the music
        audioSource.Play();

    }

    void Update()
    {
        // Move the text to the left
        Vector2 newPosition = textMeshPro.rectTransform.anchoredPosition;
        newPosition.x -= scrollSpeed * Time.deltaTime; // Scroll left
        textMeshPro.rectTransform.anchoredPosition = newPosition;

        // Update vertex positions for each letter
        var textInfo = textMeshPro.textInfo;
        if (textInfo.characterCount > 0)
        {
            textMeshPro.ForceMeshUpdate(); // Ensure the mesh is updated

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i]; // Get character info
                if (!charInfo.isVisible) continue; // Skip invisible characters

                int vertexIndex = charInfo.vertexIndex;
                int materialIndex = charInfo.materialReferenceIndex;

                // Access the vertices of the character
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

                // Calculate the time in the bounce cycle
                float time = (Time.time + i * 0.1f) % bounceDuration; // Cycle time
                float normalizedTime = time / bounceDuration; // Normalize to [0, 1]

                // Calculate the parabolic bounce offset (reverse effect)
                //float waveOffset = 4 * bounceHeight * (normalizedTime) * (normalizedTime - 1); // This creates an upward bounce
                float waveOffset = -4 * bounceHeight * (normalizedTime) * (normalizedTime - 1);


                // Update the Y position based on the offset
                vertices[vertexIndex + 0].y += waveOffset; // Top left
                vertices[vertexIndex + 1].y += waveOffset; // Top right
                vertices[vertexIndex + 2].y += waveOffset; // Bottom right
                vertices[vertexIndex + 3].y += waveOffset; // Bottom left
            }

            // Update the mesh to reflect the changes
            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

            // Reset position if the message goes off-screen
            if (newPosition.x < -textMeshPro.preferredWidth)
            {
                ResetPosition();
            }
        }
    }

    void ResetPosition()
    {
        // Start the message just outside the right side of the screen
        textMeshPro.rectTransform.anchoredPosition = new Vector2(Screen.width / 1.5f, 0);
    }
}
