import { UserContext } from "@contexts"
import { USER_ACTION } from "@contexts/UserContext"
import { IUser } from "@interfaces"
import { AppRequest } from "@requests"
import { useContext, useEffect } from "react"

export const HomePage: React.FC = () => {
    const { user, dispatch } = useContext(UserContext)

    useEffect(() => {
        fetchUserInfo()
    }, [])

    const fetchUserInfo = async () => {
        const resp = await AppRequest.getInstance().get("api/UserInfo/me")
        if (resp.data?.message === "success") {
            console.log(resp.data?.data)
            dispatch({
                type: USER_ACTION.ADD,
                payload: {
                    ...resp.data?.data
                } as IUser
            })
        }
    }
    return <>
        Home
    </>
}