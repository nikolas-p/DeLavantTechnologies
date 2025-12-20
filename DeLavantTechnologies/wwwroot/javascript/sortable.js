function sortableInit(containerId, dotNetHelper) {
    const container = document.getElementById(containerId);
    if (!container) return;

    new Sortable(container, {
        animation: 150,

        /* 🔥 КЛЮЧЕВОЕ */
        delay: 150,                 // задержка перед стартом drag (мс)
        delayOnTouchOnly: true,     // задержка только для touch (мышь мгновенно)
        touchStartThreshold: 10,     // насколько можно "шевельнуть", не начиная drag
        fallbackTolerance: 20,       // сколько px нужно сдвинуть мышь

        /* Если хочешь тянуть только за элемент */
        // handle: '.drag-drop-item',

       

        onEnd: function () {
            const newOrder = Array.from(container.children)
                .map(c => c.getAttribute('data-step-id'));

            dotNetHelper.invokeMethodAsync('UpdateOrder', newOrder)
                .catch(err => console.error(err));
        }
    });
}

window.sortableInit = sortableInit;
