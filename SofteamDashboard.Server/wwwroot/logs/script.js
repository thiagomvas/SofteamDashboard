document.addEventListener("DOMContentLoaded", () => {
    fetchLogs();
});

async function fetchLogs() {
    const token = localStorage.getItem("jwt_token"); // Retrieve JWT token from local storage

    if (!token) {
        console.error("No authentication token found. Redirecting to login...");
        return;
    }

    try {
        const response = await fetch("/api/logs?page=0&pageSize=100", {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`, // Include JWT token
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            throw new Error(`Error ${response.status}: ${response.statusText}`);
        }

        const logs = await response.json();
        console.log(logs);
        displayLogs(logs);
    } catch (error) {
        console.error("Error fetching logs:", error);
    }
}

function displayLogs(logs) {
    const tableBody = document.querySelector("#logsTable tbody");
    tableBody.innerHTML = ''; // Clear any existing rows

    logs.forEach(log => {
        const row = document.createElement("tr");

        // Determine the class for the HTTP method
        const methodClass = getMethodClass(log.method);

        row.innerHTML = `
            <td class="py-2 px-4">${log.id}</td>
            <td class="py-2 px-4"><div class="${methodClass}">${log.method}</div> ${log.path}</td>
            <td class="py-2 px-4">${log.user || 'N/A'}</td>
            <td class="py-2 px-4">${new Date(log.timestamp).toLocaleString()}</td>
            <td class="py-2 px-4">${log.body || 'N/A'}</td>
        `;

        tableBody.appendChild(row);
    });
}

// Function to return the correct class based on the HTTP method
function getMethodClass(method) {
    switch (method.toUpperCase()) {
        case 'GET':
            return 'method-get';
        case 'POST':
            return 'method-post';
        case 'PUT':
            return 'method-put';
        case 'DELETE':
            return 'method-delete';
        default:
            return '';
    }
}
