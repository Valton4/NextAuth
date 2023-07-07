import { authOptions } from "@/app/api/auth/[...nextauth]/route";
import { getServerSession } from "next-auth";

export default async function adminProfile() {
    const session = await getServerSession(authOptions);
    const token = session?.user.user.token
    const userId = session?.user.user.credentials.id
    const data = await fetch(`https://localhost:44391/api/Admin/getUsers?userId=${userId}`,
        {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                Authorization: `bearer ${token}`
            },
        })
    const res = await data.json();

    return (
        <div className="container">
            <h1>Hello World</h1>
        </div>
    )
}