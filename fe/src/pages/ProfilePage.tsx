import { EmployeeDetail, ProfileDetail } from "@components"
import { UserContext } from "@contexts/UserContext"
import { useContext, useEffect } from "react"
import { useNavigate } from "react-router"

export const ProfilePage: React.FC = () => {
    const { user } = useContext(UserContext)
    const navigate = useNavigate()

    useEffect(() => {
        if (!user) {
            setTimeout(() => {
                navigate("/home")
            }, 1000);
        }
    }, [user])

    return <>
        <div className="flex flex-col gap-2">
            <ProfileDetail user={user} />
            <EmployeeDetail user={user} />
        </div>
    </>
}
