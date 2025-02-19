/*!
    * Start Bootstrap - SB Admin v7.0.5 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2022 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
    // 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    const tableOverlay = document.querySelector('.table-overlay');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            if (tableOverlay) {
                tableOverlay.classList.toggle('menu-left');
            }
            const barsIcon = sidebarToggle.querySelector('svg.fa-bars');
            const chevronLeftIcon = sidebarToggle.querySelector('svg.fa-chevron-left');

            if (document.body.classList.contains('sb-sidenav-toggled')) {
                barsIcon.style.display = 'none';
                chevronLeftIcon.style.display = 'inline';
            } else {
                barsIcon.style.display = 'inline';
                chevronLeftIcon.style.display = 'none';
            }

            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});
