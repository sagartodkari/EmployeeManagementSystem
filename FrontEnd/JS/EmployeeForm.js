const apiBaseUrl = "http://localhost:7276/api/employee"; // Adjust URL as needed
let currentEmployeeId = null; // To store the ID for updating

// Function to submit employee data
async function submitEmployee() {
    const name = document.getElementById("employeeName").value.trim();
    const position = document.getElementById("employeePosition").value.trim();
    const salary = document.getElementById("employeeSalary").value.trim();

    // Validation for empty fields
    if (!name || !position || !salary) {
        document.getElementById("message").innerText = "Please fill in all fields.";
        return; // Exit the function if validation fails
    }

    // Check if salary is a positive number
    if (isNaN(salary) || parseFloat(salary) <= 0) {
        document.getElementById("message").innerText = "Salary must be a positive number.";
        return; // Exit the function if validation fails
    }

    const employeeData = {
        name: name,
        position: position,
        salary: parseFloat(salary),
    };

    try {
        let response;
        if (currentEmployeeId) {
            // Update existing employee
            response = await fetch(`${apiBaseUrl}/${currentEmployeeId}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(employeeData)
            });
            document.getElementById("message").innerText = "Employee updated successfully!";
        } else {
            // Create new employee
            response = await fetch(apiBaseUrl, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(employeeData)
            });
            alert("Employee added successfully!");
            window.location.href = 'EmployeeList.html'; 
        }

        if (!response.ok) throw new Error("Failed to submit employee data");

        // Reset form
        resetForm();
    } catch (error) {
        console.error(error);
        document.getElementById("message").innerText = "Error: " + error.message;
    }
}

// Function to populate form for updating an employee
function populateForm(employee) {
    document.getElementById("employeeName").value = employee.name;
    document.getElementById("employeePosition").value = employee.position;
    document.getElementById("employeeSalary").value = employee.salary;
    currentEmployeeId = employee.id;
    document.getElementById("formTitle").innerText = "Update Employee";
    document.getElementById("submitBtn").innerText = "Update Employee";
}

// Function to reset the form
function resetForm() {
    document.getElementById("employeeName").value = "";
    document.getElementById("employeePosition").value = "";
    document.getElementById("employeeSalary").value = "";
    currentEmployeeId = null;
    document.getElementById("formTitle").innerText = "Create Employee";
    document.getElementById("submitBtn").innerText = "Create Employee";
}
