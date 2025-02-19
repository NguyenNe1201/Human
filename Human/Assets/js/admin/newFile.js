//show form
if (btnDetail) {
    btnDetail.addEventListener("click", function(event) {
        var test = document.querySelectorAll(".cancel-column");
        test.forEach(element => {
            element.classList.toggle('hidden-item');
        });
    });
}
