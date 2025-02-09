async function fetchData(endpoint) {
    try {
        // Fetch data from the selected endpoint
        const response = await fetch(`/api/${endpoint}`);
        const data = await response.json();

        console.log(data); // Log the fetched data for debugging
        displayData(data);
    } catch (error) {
        console.error("Error fetching data:", error);
    }
}

// Function to display the data in the table
function displayData(data) {
    const tableBody = document.querySelector("#dataTable tbody");
    const tableHead = document.querySelector("#dataTable thead tr");

    tableBody.innerHTML = ''; // Clear any existing rows

    if (data.length === 0) {
        tableBody.innerHTML = '<tr><td colspan="5" class="text-center py-4">No data available</td></tr>';
        return;
    }

    // Get the keys (columns) from the first item of the data
    const columns = Object.keys(data[0]);

    // Update the table header
    tableHead.innerHTML = ''; // Clear existing header
    columns.forEach(col => {
        const th = document.createElement("th");
        th.classList.add("py-3", "px-4", "text-left");
        th.textContent = col.charAt(0).toUpperCase() + col.slice(1); // Capitalize first letter
        tableHead.appendChild(th);
    });

    // Add rows for each data item
    data.forEach(item => {
        const row = document.createElement("tr");

        columns.forEach(col => {
            const td = document.createElement("td");
            td.classList.add("py-2", "px-4", "border-b");

            // Check if the column is 'permissions' and format it
            if (col === "permissoes" && Array.isArray(item[col])) {
                td.textContent = item[col].map(permission => permission.nome).join(", "); // Join permission names
            } else {
                td.textContent = item[col] || 'N/A'; // Display 'N/A' if the field is empty
            }

            row.appendChild(td);
        });

        tableBody.appendChild(row);
    });
}
