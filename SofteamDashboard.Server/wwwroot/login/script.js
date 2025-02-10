document.getElementById("login-form").addEventListener("submit", async function(event) {
    event.preventDefault();

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const messageEl = document.getElementById("message");

    try {
        const response = await fetch("/api/auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        const result = await response.json();

        if (response.ok && result.token) {
            localStorage.setItem("jwt_token", result.token); // Save JWT token
            messageEl.style.color = "var(--post-request)";
            messageEl.textContent = "Login successful!";
        } else {
            messageEl.style.color = "var(--danger)";
            messageEl.textContent = result.message || "Login failed!";
        }
    } catch (error) {
        messageEl.style.color = "var(--danger)";
        messageEl.textContent = "Error connecting to server!";
    }
});
