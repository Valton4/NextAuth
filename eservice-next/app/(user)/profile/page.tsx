import { authOptions } from "@/app/api/auth/[...nextauth]/route";
import { HeadTitle } from "@/components/HeadTitle";
import { getServerSession } from "next-auth";

export default async function Profile() {
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
    console.log(res)

    return (
        <div className="">
            <HeadTitle title="Profili im" description="Edito të dhënat personale" />
            <div className="container m-auto mt-28 flex">
                <div className="left basis-1/3">

                </div>
                <div className="right basis-2/3  ">
                    <div className="p-5 bg-white rounded-md border border-gray-500">
                        {res.user.map((el: any) => (
                            <div key={el} className="card">
                                <div className="grid gap-2 p-2">
                                    <label htmlFor="userName" className="text-lg">Emri i plotë</label>
                                    <input type="text" id="userName" className="text-lg border-b-2" value={el.userName} disabled />
                                </div>
                                <div className="grid gap-2 p-2">
                                    <label htmlFor="Email" className="text-lg">Emaili</label>
                                    <input type="text" id="Email" className="text-lg border-b-2" value={el.email} disabled />
                                </div>
                                <div className="grid gap-2 p-2">
                                    <label htmlFor="phoneNumber" className="text-lg">Numri i tel</label>
                                    <input type="text" id="phoneNumber" className="text-lg border-b-2" value={el.phoneNumber} disabled />
                                </div>
                                <div className="grid gap-2 p-2">
                                    <label htmlFor="currentPassword" className="text-lg">Fjalkalimi aktual</label>
                                    <input type="text" id="currentPassword" className="text-lg border-b-2" placeholder="ju lutem vendosni fjalkalimin aktual" />
                                </div>
                            </div>
                        ))}
                    </div>

                </div>

            </div>
        </div>
    )
}