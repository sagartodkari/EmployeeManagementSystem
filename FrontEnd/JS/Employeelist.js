var apiBaseUrl = "http://localhost:7276/api/Employee"; // Adjust the URL as needed

// Function to populate employee data
function populateEmployeeTable() {
    const xhr = new XMLHttpRequest();
    const url = apiBaseUrl + "/EmployeeList";
    xhr.open("GET", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    
    xhr.onload = function () {
        if (xhr.status >= 200 && xhr.status < 300) {
            const employees = JSON.parse(xhr.responseText);
            
            const tbody = document.querySelector("#employeeTable tbody");
            tbody.innerHTML = ""; // Clear existing rows

            employees.forEach(emp => {
                const row = document.createElement("tr");
                row.innerHTML = `
                    <td><input type="checkbox" value="${emp.id}"></td>
                    <td>${emp.name}</td>
                    <td>${emp.position}</td>
                    <td>${emp.salary}</td>
                    <td>
                        <button onclick="editEmployee(${emp.id})">✏️</button>
                    </td>
                `;
                tbody.appendChild(row);
            });
        } else {
            console.error("Failed to fetch employees:", xhr.statusText);
        }
    };

    xhr.onerror = function () {
        console.error("Request failed.");
    };

    xhr.send();
}

// Add Employee button click
document.getElementById("addEmployeeBtn").addEventListener("click", () => {
    window.location.href = 'CreateEmployee.html'; // Adjust the path as necessary
});

// Delete selected employees
document.getElementById("deleteEmployeeBtn").addEventListener("click", async () => {
    const checkboxes = document.querySelectorAll("#employeeTable tbody input[type='checkbox']:checked");
    const idsToDelete = Array.from(checkboxes).map(checkbox => parseInt(checkbox.value));

    if (idsToDelete.length === 0) {
        alert("Please select at least one employee to delete.");
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/deleteMultiple`, {
            method: "POST", // Use POST or DELETE depending on your design
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(idsToDelete)
        });

        if (!response.ok) {
            throw new Error("Failed to delete employees");
        }

        alert("Employees deleted successfully!");
        populateEmployeeTable(); // Refresh the table
    } catch (error) {
        console.error(error);
        alert("Error: " + error.message);
    }
});


// Edit Employee
function editEmployee(empId) {
    window.location.href = `UpdateEmployee.html?id=${empId}`; // Redirect with employee ID
}

// Initial population of the employee table
populateEmployeeTable();
