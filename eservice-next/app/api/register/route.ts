import { NextRequest, NextResponse } from "next/server";


export async function POST(request: Request) {

    const { Role, Email, Password, UserName } = await request.json();
    console.log(Role)
    const res = await fetch('https://localhost:44391/api/Authentication/Register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Role: Role,
            Email: Email,
            Password: Password,
            UserName: UserName,
        }),
    });
    if (res.ok) {
        const data = await res.json();
        if (data.ok)
            return NextResponse.json(data);
        if (!data.ok)
            return NextResponse.json("error")
    }
}
