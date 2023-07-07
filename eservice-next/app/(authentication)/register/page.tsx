import RegisterPage from "./Register";
export default async function Register() {
    const res = await fetch('https://localhost:44391/api/Authentication/GetRoles');
    const roles = await res.json();
    return (
        <>
            <RegisterPage
                roles={roles}
            />
        </>
    )
}