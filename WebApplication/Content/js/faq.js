document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".button_drop");

    buttons.forEach(button => {
        const arrow = button.querySelector(".img_arrow");
        
        button.addEventListener("click", function () {
            const dropdown = this.closest(".dropdown");
            const content = dropdown.querySelector(".dropdown-content");

            // Переключаем текущий dropdown
            dropdown.classList.toggle("show");

            // Динамически устанавливаем max-height
            if (dropdown.classList.contains("show")) {
                content.style.maxHeight = content.scrollHeight + "px"; // Высота контента
            } else {
                content.style.maxHeight = "0px";
            }
        });

        // Анимация стрелки при наведении с плавным переходом
        if (arrow) {
            arrow.style.transition = "transform 0.3s ease-in-out";
        }
        
        button.addEventListener("mouseenter", function () {
            if (arrow) arrow.style.transform = "translateX(10px)";
        });
        button.addEventListener("mouseleave", function () {
            if (arrow) arrow.style.transform = "translateX(0)";
        });
    });

    // Закрытие при клике вне всех dropdown'ов
    document.addEventListener("click", function (event) {
        if (!event.target.closest(".dropdown")) {
            document.querySelectorAll(".dropdown").forEach(d => {
                d.classList.remove("show");
                d.querySelector(".dropdown-content").style.maxHeight = "0px";
            });
        }
    });
});
