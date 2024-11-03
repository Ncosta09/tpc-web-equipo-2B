document.addEventListener('DOMContentLoaded', function () {
    var profileMenu = document.querySelector('.profile-menu');
    var dropdownMenu = document.querySelector('.dropdown-menu');

    if (profileMenu && dropdownMenu) {
        profileMenu.addEventListener('click', function (e) {
            e.stopPropagation();
            dropdownMenu.style.display = dropdownMenu.style.display === 'block' ? 'none' : 'block';
        });

        document.addEventListener('click', function () {
            dropdownMenu.style.display = 'none';
        });

        dropdownMenu.addEventListener('click', function (e) {
            e.stopPropagation();
        });
    }
});