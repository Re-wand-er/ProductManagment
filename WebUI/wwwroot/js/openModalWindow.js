function openModal() {

    document.getElementById('modal').style.display = 'block';

}

function closeModal() {

    document.getElementById('modal').style.display = 'none';

}

document.getElementById('openModalBtn').addEventListener('click', openModal);

document.getElementById('closeModalBtn').addEventListener('click', closeModal);

window.addEventListener('click', function (event) {

    if (event.target === document.getElementById('modal')) {

        closeModal();

    }

});

window.addEventListener('beforeunload', function (event) {

    if (document.getElementById('modal').style.display === 'block') {

        closeModal();

    }

});