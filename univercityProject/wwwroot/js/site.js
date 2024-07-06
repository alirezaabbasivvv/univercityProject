// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const menu = document.getElementById("hamburger-menu");
const sidebar = document.getElementById("sidebar");
const sidebarShadow = document.getElementById("sidebar-shadow");
function showMenu() {
    sidebar.style.display = "flex";
    sidebarShadow.style.display = "flex";
}
function hideMenu() {
    sidebar.style.display = "none";
    sidebarShadow.style.display = "none";
}
sidebarShadow.addEventListener("click", hideMenu);
menu.addEventListener("click", showMenu);

function responsiveStyles() {
    if (window.innerWidth > 600) {
        sidebarShadow.style.display = "flex";
        sidebar.style.display = "flex";
    } else {
        sidebar.style.display = "none";
        sidebarShadow.style.display = "none";
    }
}

// Apply styles on window resize
window.addEventListener("resize", responsiveStyles);