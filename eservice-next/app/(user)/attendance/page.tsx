import { HeadTitle } from '@/components/HeadTitle'
import { authOptions } from '@/app/api/auth/[...nextauth]/route';
import { getServerSession } from "next-auth";
export default async function attendancePage() {
    return (
        <>
            <HeadTitle title="Vijueshmëria" description="Pasqyrimi i prezencës tuaj" />
            <h1>Attendance</h1>
        </>
    )
}