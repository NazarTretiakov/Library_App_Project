document.addEventListener('DOMContentLoaded', () => {

    const container = document.querySelector('.profile-items_user-information-items_subscribe-button');

    if (!container) {
        return;
    }


    container.addEventListener('click', async (e) => {

        const subscribeButton = e.target.closest('.profile-items_user-information-items_subscribe-button-element');

        if (subscribeButton) {
            const userId = subscribeButton.getAttribute('data-user-id');
            if (userId) {
                await toggleLike(userId, container);
            }
        }
    });

    async function toggleLike(userId, container) {
        try {
            const response = await fetch('/user-profile/toggle-subscription', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ userId })
            });

            if (response.ok) {
                const partialHtml = await response.text();
                container.innerHTML = partialHtml;
            } else {
                console.error('Error while toggling subscription');
            }
        } catch (error) {
            console.error('Connection error:', error);
        }
    }
});