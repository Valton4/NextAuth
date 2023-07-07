import { getServerSession } from 'next-auth'
import Image from 'next/image'
import { authOptions } from './api/auth/[...nextauth]/route';
import { loggedInUser } from '@/types/interfaces';

export default async function Home() {

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
    <main className="m-4 ">
      <div className=' bg-slate-800 text-white rounded-md p-4' >
        {res.user.map((user: any) => (
          <div>
            <div className=' flex gap-2' key={user.id}>
              <h1>Id:</h1>
              <h1> {user.id}</h1></div>
            <div className='flex gap-2'>
              <h1>Emaili:</h1>
              <h1>{user.email}</h1></div>
            <div className='flex gap-2'>
              <h1>Passwordi:</h1>
              <h1>{user.passwordHash}</h1></div>
          </div>

        ))}
        {res.roles.map((role: any) => (
          <div className='flex gap-2' key={role}>
            <h1>Roli:</h1>
            <h1 >{role}</h1>
          </div>
        ))}
      </div>


    </main>
  )
}
