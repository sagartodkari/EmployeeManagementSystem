const apiUrl = "http://localhost:7276/api/Login"; // Change to your actual API URL
let token = "";

async function login() {
    const username = document.getElementById("loginUsername").value.trim();
    const password = document.getElementById("loginPassword").value.trim();

    // Validation for empty fields
    if (!username || !password) {
        document.getElementById("loginMessage").innerText = "Please enter both username and password.";
        return; // Exit the function if validation fails
    }

    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ Username: username, Password: password })
        });

        if (response.ok) {
            const data = await response.json();
            token = data.token; // Store the token for later use
            localStorage.setItem("token", token); // Store the token
            document.getElementById("loginMessage").innerText = "Login successful!";
            // Redirect to another page or perform other actions
            window.location.href = 'EmployeeList.html'; // Redirect
        } else {
            const errorData = await response.json();
            document.getElementById("loginMessage").innerText = errorData.message || "Login failed.";
        }
    } catch (error) {
        console.error("Error during login:", error);
        document.getElementById("loginMessage").innerText = "An error occurred. Please try again.";
    }
}

// Attach the login function to the button click event
document.getElementById("loginButton").addEventListener("click", login);
