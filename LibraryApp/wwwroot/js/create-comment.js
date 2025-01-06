document.addEventListener('DOMContentLoaded', () => {
    const addCommentIcon = document.querySelector('.post-items-create-comment-items-creation-items-button-element-image path');
    const createCommentButton = document.querySelector('.post-items-create-comment-items-creation-items-button-element');
    const inputForContentOfComment = document.querySelector('.post-items-create-comment-items-creation-items-input-element');


    createCommentButton.addEventListener('click', async (e) => {
        const postId = createCommentButton.getAttribute('data-post-id');
        const content = inputForContentOfComment.value;

        if (postId && content.trim()) {
            await createComment(postId, content);
        }
    });


    async function createComment(postId, content) {
        try {
            const response = await fetch('/forum/post/create-comment', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ postId, content })
            });

            if (!response.ok) {
                console.error('Error:', response.statusText);
            } else {
                inputForContentOfComment.value = '';
                addCommentIcon.setAttribute('fill', '#D9D9D9');

                const newCommentHtml = await response.text();

                const commentsContainer = document.querySelector('.post-items-comments-container');

                if (commentsContainer) {
                    commentsContainer.innerHTML = newCommentHtml;
                }
            }
        } catch (error) {
            console.error('Connection error:', error);
        }
    }


    inputForContentOfComment.addEventListener('input', () => {
        if (inputForContentOfComment.value.trim().length > 0) {
            addCommentIcon.setAttribute('fill', '#000000');
        } else {
            addCommentIcon.setAttribute('fill', '#D9D9D9');
        }
    });
});