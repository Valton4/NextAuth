'use client'
import { signIn } from "next-auth/react"
import { useState } from "react"
import { useRouter } from "next/navigation";
import Link from "next/link";
export default function LoginForm() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const redirect = useRouter();

    async function Submit(e: any) {
        e.preventDefault()
        const data = await signIn('credentials', {
            email: email,
            password: password,
            redirect: false
        })
        if (data?.ok) {
            redirect.push('/home')
        }

    }


    return <div className="container row m-auto items-center justify-center vh-100">
        <form className="col-lg-4"  onSubmit={Submit} method="post">
            <div className="mb-2">
                <label htmlFor="email">Email</label>
                <input
                    type="email"
                    name="email"
                    id="email"
                    className="form-control"
                    onChange={(e) => setEmail(e.currentTarget.value)} placeholder="put your email" />
            </div>
            <div className="mb-2">
                <label htmlFor="">Password</label>
                <input
                    type="password"
                    name="password"
                    id="password"
                    className="form-control"
                    onChange={(e) => setPassword(e.currentTarget.value)} placeholder="put your password" />
            </div>
            <button className="btn btn-primary w-100" type="submit">Login</button>
            <p>Dont have an account? <Link href="/register">Register</Link></p>
        </form>
    </div>
}