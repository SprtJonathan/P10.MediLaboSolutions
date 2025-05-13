let noteIdToDelete = null;

document.addEventListener("DOMContentLoaded", function () {
    console.log("note-scripts.js chargé");

    bindNoteActions();

    const form = document.getElementById("add-note-form");
    if (form) {
        form.addEventListener("submit", function (e) {
            e.preventDefault();
            const formData = new FormData(form);
            const data = new URLSearchParams();
            for (const pair of formData) {
                data.append(pair[0], pair[1]);
            }

            fetch("/Notes/Create", {
                method: "POST",
                body: data
            }).then(response => {
                if (response.ok) {
                    const patientId = form.querySelector('input[name="PatientId"]').value;
                    refreshNoteList(patientId);
                    form.reset();
                } else {
                    alert("Erreur lors de l'ajout de la note.");
                }
            });
        });
    }
});

function bindNoteActions() {
    document.querySelectorAll(".edit-note").forEach(button => {
        button.addEventListener("click", () => {
            const id = button.dataset.id;
            const container = document.getElementById(`note-${id}`);
            container.querySelector(".note-text").style.display = "none";
            container.querySelector(".edit-form").style.display = "block";
        });
    });

    document.querySelectorAll(".cancel-edit").forEach(button => {
        button.addEventListener("click", () => {
            const container = button.closest(".note-item");
            container.querySelector(".edit-form").style.display = "none";
            container.querySelector(".note-text").style.display = "block";
        });
    });

    document.querySelectorAll(".save-edit").forEach(button => {
        button.addEventListener("click", () => {
            const id = button.dataset.id;
            const container = document.getElementById(`note-${id}`);
            const newText = container.querySelector(".edit-textarea").value;

            fetch("/Notes/Edit", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: new URLSearchParams({ id, texte: newText })
            }).then(response => {
                if (response.ok) {
                    container.querySelector(".note-text").textContent = newText;
                    container.querySelector(".edit-form").style.display = "none";
                    container.querySelector(".note-text").style.display = "block";
                } else {
                    alert("Erreur lors de la modification.");
                }
            });
        });
    });

    document.querySelectorAll(".delete-note").forEach(button => {
        button.addEventListener("click", () => {
            noteIdToDelete = button.dataset.id;
        });
    });

    const confirmDeleteBtn = document.getElementById("confirmDeleteBtn");
    if (confirmDeleteBtn) {
        confirmDeleteBtn.addEventListener("click", () => {
            if (!noteIdToDelete) return;

            fetch("/Notes/Delete", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: new URLSearchParams({ id: noteIdToDelete })
            }).then(response => {
                if (response.ok) {
                    const toRemove = document.getElementById(`note-${noteIdToDelete}`);
                    if (toRemove) toRemove.remove();
                    const modal = bootstrap.Modal.getInstance(document.getElementById("deleteModal"));
                    modal.hide();
                } else {
                    alert("Erreur lors de la suppression.");
                }
            });
        });
    }
}

function refreshNoteList(patientId) {
    const container = document.getElementById("note-list");
    if (!container) return;

    fetch(`/Patients/Details/${patientId}`)
        .then(res => res.text())
        .then(html => {
            const temp = document.createElement("div");
            temp.innerHTML = html;
            const updated = temp.querySelector("#note-list");
            container.innerHTML = updated.innerHTML;
            bindNoteActions();
        });
}
