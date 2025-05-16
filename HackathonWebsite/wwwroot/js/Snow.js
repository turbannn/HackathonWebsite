document.addEventListener("DOMContentLoaded", function () {
    const snowContainer = document.createDocumentFragment();

    for (let i = 0; i < 200; i++) {
        const snowflake = document.createElement('div');
        snowflake.classList.add('snow');

        const size = Math.random() * 4 + 2;
        const left = Math.random() * window.innerWidth;
        const delay = Math.random() * 10;
        const duration = Math.random() * 10 + 5;

        snowflake.style.width = `${size}px`;
        snowflake.style.height = `${size}px`;
        snowflake.style.left = `${left}px`;
        snowflake.style.animationDuration = `${duration}s`;
        snowflake.style.animationDelay = `${delay}s`;

        snowContainer.appendChild(snowflake);
    }

    document.body.appendChild(snowContainer);
});
