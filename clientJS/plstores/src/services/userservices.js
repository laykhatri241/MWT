export async function getAllUsers() {

    const response = await fetch(' https://localhost:5001/MWT/SignUp');
    return await response.json();
}

export async function createUser(data) {
    const response = await fetch(` https://localhost:5001/MWT/SignUp`, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({user: data})
      })
    return await response.json();
}