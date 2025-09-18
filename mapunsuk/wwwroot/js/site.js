// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', (event) => {
    // Remove any code that might be preventing default link behavior, for example:
    /*
    const links = document.querySelectorAll('a.add-post-btn');
    links.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault(); // This line would stop the navigation
            console.log('Button click was intercepted.');
        });
    });
    */

    const userDropdownToggle = document.getElementById('userDropdownToggle');
    const userDropdownMenu = document.getElementById('userDropdownMenu');

    if (userDropdownToggle && userDropdownMenu) {
        userDropdownToggle.addEventListener('click', function (event) {
            event.stopPropagation();
            userDropdownMenu.classList.toggle('show');
        });

        // Close the dropdown if the user clicks outside of it
        window.addEventListener('click', function (event) {
            if (!userDropdownMenu.contains(event.target) && !userDropdownToggle.contains(event.target)) {
                if (userDropdownMenu.classList.contains('show')) {
                    userDropdownMenu.classList.remove('show');
                }
            }
        });
    }
});
