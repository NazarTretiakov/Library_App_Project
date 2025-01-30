document.addEventListener('DOMContentLoaded', () => {
    
    const container = document.querySelector('.book-items_save-icon');

    if (!container) {
        return;
    }

    
    container.addEventListener('click', async (e) => {
        
        const saveButton = e.target.closest('.book-items_save-icon-element');

        if (saveButton) {
            const bookId = saveButton.getAttribute('data-book-id');
            if (bookId) {
                await toggleSave(bookId, container);
            }
        }
    });

    async function toggleSave(bookId, container) {
        try {
            const response = await fetch('/library/book/toggle-book-save', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ bookId })
            });

            if (response.ok) {
                const partialHtml = await response.text();
                container.innerHTML = partialHtml;
            } else {
                console.error('Error while toggling save');
            }
        } catch (error) {
            console.error('Connection error:', error);
        }
    }
});