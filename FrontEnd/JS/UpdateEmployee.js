const apiBaseUrl = "http://localhost:7276/api/Employee"; // Adjust URL as needed
let currentEmployeeId = null; // To store the ID for updating

// Function to fetch employee data by ID
async function fetchEmployee(id) {
    try {
        const response = await fetch(`${apiBaseUrl}/${id}`);
        if (!response.ok) throw new Error("Failed to fetch employee data");

        const employee = await response.json();
        populateForm(employee);
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
    currentEmployeeId = employee.id; // Store the ID for later use
}

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
        id: currentEmployeeId,
        name: name,
        position: position,
        salary: parseFloat(salary),
    };

    try {
        const response = await fetch(`${apiBaseUrl}/${currentEmployeeId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(employeeData)
        });

        if (!response.ok) throw new Error("Failed to update employee data");

        alert("Employee updated successfully!"); // Show alert
        window.location.href = 'EmployeeList.html'; // Redirect to employee list page
    } catch (error) {
        console.error(error);
        document.getElementById("message").innerText = "Error: " + error.message;
    }
}

// Call fetchEmployee with the desired employee ID from URL parameters
const urlParams = new URLSearchParams(window.location.search);
const employeeId = urlParams.get('id'); // Get the ID from the URL
if (employeeId) {
    fetchEmployee(employeeId); // Fetch the employee data
}
