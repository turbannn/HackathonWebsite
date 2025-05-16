async function submitTask() {
    console.log("Function submitTask executed");

    const id = parseInt(document.getElementById("TaskId").value) || 0;
    const rating = parseInt(document.getElementById("TaskRating").value) || 0;
    const name = document.getElementById("TaskName").value.trim();
    const description = document.getElementById("TaskDescription").value.trim();
    
    if (!name) {
        alert("Error: Name cant be empty");
        return;
    }
    if (!description) {
        alert("Error: Description cant be empty");
        return;
    }

    const task = {
        Id: id, Name: name, Description: description, Rating: rating
    };

    console.log("Sending data:", task);

    fetch("/Tasks/CreateTask", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(task)
    })
        .then(response => response.json())
        .then(result => {
            console.log("Server response:", result);

            if (result.success) {
                window.location.href = result.redirectUrl;
            } else {
                alert("Save error: " + (result.message || "Unknown error"));
            }
        })
        .catch(error => {
            console.error("Request sending error:", error);
            alert("Data sending error");
        });
}

async function updateTask() {
    console.log("Function updateTask executed");

    const id = parseInt(document.getElementById("TaskId").value) || 0;
    const rating = parseInt(document.getElementById("TaskRating").value) || 0;
    const name = document.getElementById("TaskName").value.trim();
    const description = document.getElementById("TaskDescription").value.trim();

    if (!id || !name || !description) {
        alert("Error: fill all fields");
        return;
    }

    let task = {
        Id: parseInt(id),
        Rating: parseInt(rating),
        Name: name.trim(),
        Description: description.trim()
    };

    console.log("JSON payload:", JSON.stringify(task));
    console.log("Sending data to server:", task);

    try {
        const response = await fetch(`/Tasks/EditTask/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(task)
        });

        const result = await response.json();
        console.log("Server response:", result);

        if (result.success) {
            window.location.href = result.redirectUrl;
        } else {
            alert("Update error: " + (result.message || "Unknown error"));
        }
    } catch (error) {
        console.error("Request sending error:", error);
        alert("Data update error");
    }
}
async function loadTask(id) {
    console.log("Loading expense with ID:", id);

    try {
        const response = await fetch(`/Tasks/GetTask/${id}`);
        if (!response.ok) throw new Error("Data loading error");

        const task = await response.json();
        console.log("Data received:", task);

        if (!task) {
            alert("Error: Data not found");
            return;
        }

    } catch (error) {
        console.error("Request sending error:", error);
        alert("Data update error");
    }
}

async function deleteTask(id) {
    console.log("Function deleteExpense executed");

    try {
        const response = await fetch(`/Tasks/DeleteTask/${id}`, { method: "DELETE" });
        const result = await response.json();

        console.log("Deleting expense with id:", id);

        if (result.success) {
            window.location.href = result.redirectUrl;
        } else {
            alert("Delete error: " + (result.message || "Unknown error"));
        }
    } catch (error) {
        console.error("Request sending error:", error);
        alert("Deleting error");
    }
}

function submitRating(taskId) {
    const ratingInput = document.getElementById(`ratingInput_${taskId}`);
    const ratingValue = ratingInput.value;

    if (!ratingValue || ratingValue < 0 || ratingValue > 100) {
        alert("Please enter a valid rating between 0 and 100.");
        return;
    }

    fetch("/Tasks/Rate", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            id: taskId,
            rating: parseInt(ratingValue)
        })
    })
        .then(response => response.json())
        .then(result => {
            console.log("Server response:", result);

            if (result.success) {
                window.location.href = result.redirectUrl;
            } else {
                alert("Save error: " + (result.message || "Unknown error"));
            }
        })
        .catch(error => {
            console.error("Request sending error:", error);
            alert("Data sending error");
        });
}

function submitRatingDetails() {
    const ratingValue = document.getElementById("ratingInput").value;
    const taskId = document.getElementById("TaskId").value;

    if (!ratingValue || ratingValue < 0 || ratingValue > 100) {
        alert("Please enter a valid rating between 0 and 100.");
        return;
    }

    fetch("/Tasks/Rate", {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            id: taskId,
            rating: parseInt(ratingValue)
        })
    })
        .then(response => response.json())
        .then(result => {
            console.log("Server response:", result);

            if (result.success) {
                window.location.href = result.redirectUrl;
            } else {
                alert("Save error: " + (result.message || "Unknown error"));
            }
        })
        .catch(error => {
            console.error("Request sending error:", error);
            alert("Data sending error");
        });
}

function toggleRatePopup(taskId) {
    const popup = document.getElementById(`ratePopup_${taskId}`);
    popup.classList.toggle("show");
}

function toggleRatePopupDetails() {
    const popup = document.getElementById("ratePopup");
    popup.classList.toggle("show");
}

function RedirectToTaskDetailedView(taskId) {
    window.location.href = `/Tasks/DetailedTask/${taskId}`;
}

function RedirectToTaskCreation() {
    window.location.href = "/Tasks/CreateTask";
}

function RedirectToTaskEditing(taskId) {
    window.location.href = `/Tasks/EditTask/${taskId}`;
}