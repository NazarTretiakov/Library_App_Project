document.addEventListener('DOMContentLoaded', () => {
    
    const container = document.querySelector('.post-items-create-comment-items-interaction-items-like');

    if (!container) {
        return;
    }

    
    container.addEventListener('click', async (e) => {
        
        const likeButton = e.target.closest('.post-items-create-comment-items-interaction-items-like-element');

        if (likeButton) {
            const postId = likeButton.getAttribute('data-post-id');
            if (postId) {
                await toggleLike(postId, container);
            }
        }
    });

    async function toggleLike(postId, container) {
        try {
            const response = await fetch('/forum/post/toggle-like', {
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
                console.error('Error while toggling like');
            }
        } catch (error) {
            console.error('Connection error:', error);
        }
    }
});