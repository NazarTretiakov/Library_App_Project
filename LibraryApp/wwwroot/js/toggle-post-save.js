document.addEventListener('DOMContentLoaded', () => {
    
    const container = document.querySelector('.post-items-create-comment-items-interaction-items-save');

    if (!container) {
        return;
    }

    
    container.addEventListener('click', async (e) => {
        
        const saveButton = e.target.closest('.post-items-create-comment-items-interaction-items-save-element');

        if (saveButton) {
            const postId = saveButton.getAttribute('data-post-id');
            if (postId) {
                await toggleSave(postId, container);
            }
        }
    });

    async function toggleSave(postId, container) {
        try {
            const response = await fetch('/forum/post/toggle-post-save', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ postId })
            });

            if (response.ok) {
                const partialHtml = await response.text();
                container.innerHTML = partialHtml;
            } else {
                console.error("Error while toggling save");
            }
        } catch (error) {
            console.error("Connection error:", error);
        }
    }
});