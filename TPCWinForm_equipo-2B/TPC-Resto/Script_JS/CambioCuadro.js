document.addEventListener("DOMContentLoaded", function () {
    const menuItems = document.querySelectorAll(".menu-left ul li a");
    const dashboards = document.querySelectorAll(".reports-dashboard > div");

    menuItems.forEach((item, index) => {
        item.addEventListener("click", function (e) {
            e.preventDefault();

            dashboards.forEach(dash => dash.classList.remove("active"));
            dashboards[index].classList.add("active");
        });
    });

    dashboards[0].classList.add("active");
});