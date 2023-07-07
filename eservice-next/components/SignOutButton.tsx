'use client'
import { signOut } from "next-auth/react"
export const SignOutButton = () => {
    return <>
        <button onClick={(e) => {
            e.preventDefault();
            signOut()
        }}>SignOut</button>
    </>
}